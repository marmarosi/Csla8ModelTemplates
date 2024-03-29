using Csla8RestApi.Tests.SourceGenerator.Models;
using System.Xml;
using System.Xml.Linq;

namespace Csla8RestApi.Tests.SourceGenerator
{
    internal static class Source
    {
        public static void Generate(
            string mapPath,
            string wrapperRoot,
            BaseData data
            )
        {
            var map = GetMap(mapPath, data);
            Console.WriteLine(map.ShortSource);

            var xml = XDocument.Load(map.SnippetPath);
            var snippet = (XCData)xml.Root.DescendantNodes()
                .Where(x => x.NodeType == XmlNodeType.CDATA)
                .First();
            var source = snippet.Value;

            foreach(var model in map.Models)
                source = source.Replace($"${model.Placeholder}$", model.Name);
            source = source.Replace("$end$", "");

            var wrapper = GetWrapper(wrapperRoot, map.ShortSource);
            var content = wrapper.Replace("$snippet$", source);
            SaveFile(map.SourcePath, content);
        }

        private static Map GetMap(
            string mapPath,
            BaseData data
            )
        {
            var map = new Map();
            var project = "";
            var sourceFolder = "";
            var fileName = "";
            var lines = File.ReadLines(mapPath);
            foreach (var line in lines)
            {
                var ln = line.Trim();
                var ix = ln.IndexOf(':');
                if (ix < 1)
                    continue;
                var key = ln.Substring(0, ix).Trim();
                if (key.Length == 0)
                    continue;
                var value = ln.Substring(ix + 1).Trim();
                if (value.Length == 0)
                    continue;

                switch (key)
                {
                    case "Snippet":
                        var values = value.Split('\\');
                        sourceFolder = values[0].Replace('_', '\\');
                        map.SnippetPath = Path.Combine(data.SnippetBasePath, value);
                        break;
                    case "Project":
                        project = value;
                        break;
                    case "FileName":
                        fileName = value;
                        switch (project)
                        {
                            case "Contract":
                                map.SourcePath = data.ContractBasePath;
                                break;
                            case "DAL":
                                map.SourcePath = data.DalBasePath;
                                break;
                            case "Model":
                                map.SourcePath = data.ModelBasePath;
                                break;
                            case "Controller":
                                map.SourcePath = data.ControllerBasePath;
                                break;
                        }
                        break;
                    default:
                        map.Models.Add(new Model
                        {
                            Placeholder = key,
                            Name = value
                        });
                        break;
                }
            }
            var model = map.Models.Find(o => o.Placeholder == "ROOT_MODEL");
            if (model != null)
                fileName = fileName.Replace("===", model.Name);
            model = map.Models.Find(o => o.Placeholder == "CHILD_MODEL");
            if (model != null)
                fileName = fileName.Replace("---", model.Name);
            model = map.Models.Find(o => o.Placeholder == "COMMAND_MODEL");
            if (model != null)
                fileName = fileName.Replace("+++", model.Name);
            fileName += ".cs";

            map.ShortSource = Path.Combine(sourceFolder, fileName);
            map.SourcePath = Path.Combine(map.SourcePath, map.ShortSource);
            return map;
        }

        private static SnippetType GetSnippetType(
            string snippetPath
            )
        {
            var fileName = Path.GetFileName(snippetPath);
            if (fileName.StartsWith("C_"))
                return SnippetType.Contract;
            if (fileName.StartsWith("D_"))
                return SnippetType.Dal;
            if (fileName.StartsWith("M_"))
                return SnippetType.Model;
            if (fileName.EndsWith("Controller"))
                return SnippetType.Controller;
            return SnippetType.None;
        }

        private static string GetWrapper(
            string wrapperRoot,
            string shortSource
            )
        {
            var wrapperPath = Path.Combine(wrapperRoot, shortSource);
            CheckFolder(Path.GetDirectoryName(wrapperPath));

            wrapperPath = Path.Combine(
                Path.GetDirectoryName(wrapperPath),
                Path.GetFileNameWithoutExtension(wrapperPath) + ".txt"
                );
            return File.ReadAllText(wrapperPath);
        }

        private static void SaveFile(
            string filePath,
            string content
            )
        {
            //CheckFolder(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, content);
        }

        private static void CheckFolder(
            string? folderPath
            )
        {
            if (folderPath != null && !Directory.Exists(folderPath))
            {
                var parent = Directory.GetParent(folderPath); 
                if (parent != null)
                    CheckFolder(parent.FullName);
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}
