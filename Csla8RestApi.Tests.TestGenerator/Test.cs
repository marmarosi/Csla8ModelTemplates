using Csla8RestApi.Tests.TestGenerator.Models;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Csla8RestApi.Tests.TestGenerator
{
    internal static class Test
    {
        public static void Generate(
            string mapPath,
            string wrapperRoot,
            BaseData data
            )
        {
            var testMap = GetMap(mapPath, data);
            Console.WriteLine(testMap.ShortTest);

            var xml = XDocument.Load(testMap.SnippetPath);
            var snippet = (XCData)xml.Root!.DescendantNodes()
                .First(x => x.NodeType == XmlNodeType.CDATA);
            var source = snippet.Value;

            foreach (var textSwap in testMap.TextSwaps)
            {
                var count = Regex.Matches(source, $"\\${textSwap.Placeholder}\\$").Count;
                source = source.Replace($"${textSwap.Placeholder}$", textSwap.Name);
                if (count == 0)
                    Console.WriteLine($"    {testMap.ShortWrapper} - {textSwap.Placeholder}: no occurrences");
            }
            source = source.Replace("$end$", "");

            var wrapper = GetWrapper(wrapperRoot, testMap.ShortWrapper);
            var content = wrapper.Replace("$snippet$", source);
            SaveFile(testMap.TestPath, content);
        }

        private static TestMap GetMap(
            string mapPath,
            BaseData data
            )
        {
            var testMap = new TestMap();
            var project = "";
            var testFolder = "";
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
                        testFolder = values[0].Replace('_', '\\');
                        testMap.SnippetPath = Path.Combine(data.SnippetBasePath, value);
                        testMap.ShortWrapper = value.Replace(".snippet", ".txt");
                        break;
                    case "Project":
                        project = value;
                        break;
                    case "FileName":
                        fileName = value;
                        switch (project)
                        {
                            case "Contract":
                                testMap.TestPath = data.ContractBasePath;
                                break;
                            case "DAL":
                                testMap.TestPath = data.DalBasePath;
                                break;
                            case "Model":
                                testMap.TestPath = data.ModelBasePath;
                                break;
                            case "WebApi":
                                testMap.TestPath = data.ControllerBasePath;
                                break;
                        }
                        break;
                    default:
                        testMap.TextSwaps.Add(new TextSwap
                        {
                            Placeholder = key,
                            Name = value
                        });
                        break;
                }
            }
            var model = testMap.TextSwaps.Find(o => o.Placeholder == "ROOT_MODEL");
            if (model != null)
                fileName = fileName.Replace("===", model.Name);
            model = testMap.TextSwaps.Find(o => o.Placeholder == "CHILD_MODEL");
            if (model != null)
                fileName = fileName.Replace("---", model.Name);
            model = testMap.TextSwaps.Find(o => o.Placeholder == "COMMAND_MODEL");
            if (model != null)
                fileName = fileName.Replace("+++", model.Name);
            fileName += ".cs";

            testMap.ShortTest = Path.Combine(testFolder, fileName);
            testMap.TestPath = Path.Combine(testMap.TestPath, testMap.ShortTest);
            return testMap;
        }

        private static string GetWrapper(
            string wrapperRoot,
            string shortWrapper
            )
        {
            var wrapperPath = Path.Combine(wrapperRoot, shortWrapper);
            CheckFolder(Path.GetDirectoryName(wrapperPath));

            wrapperPath = Path.Combine(
                Path.GetDirectoryName(wrapperPath)!,
                Path.GetFileNameWithoutExtension(wrapperPath) + ".txt"
                );
            return File.ReadAllText(wrapperPath);
        }

        private static void SaveFile(
            string filePath,
            string content
            )
        {
            CheckFolder(Path.GetDirectoryName(filePath));
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
