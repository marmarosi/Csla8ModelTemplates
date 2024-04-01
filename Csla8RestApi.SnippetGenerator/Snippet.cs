using Csla8RestApi.SnippetGenerator.Models;
using System.Text;

namespace Csla8RestApi.SnippetGenerator
{
    internal static class Snippet
    {
        public static void Generate(
            string snippetMapPath,
            BaseData data
            )
        {
            // Get mapping data to convert source to snippet.
            var snippetMap = GetMap(snippetMapPath, data);
            Console.WriteLine(snippetMap.ShortTarget);

            // Compose literals.
            var sb = new StringBuilder();
            foreach (var textSwap in snippetMap.TextSwaps)
                sb.Append(data.LiteralTemplate
                    .Replace("$id$", textSwap.To)
                    .Replace("$tooltip$", textSwap.ToolTip)
                    .Replace("$default$", textSwap.DefaultValue)
                    );
            var literals = sb.ToString();

            // Read source.
            var sourceLines = GetSource(snippetMap.SourcePath);
            foreach (var textSwap in snippetMap.TextSwaps)
                for (int i = 0; i < sourceLines.Count; i++)
                {
                    var line = sourceLines[i];
                    var isComment = line.TrimStart().StartsWith("///");
                    if (textSwap.InComment && isComment ||
                        !textSwap.InComment && !isComment
                        )
                    {
                        sourceLines[i] = line.Replace(textSwap.From, $"${ textSwap.To }$");
                    }
                }
            var source = string.Join("\r\n", sourceLines);

            // Compose and save snippet.
            var snippet = data.SnippetTemplate
                .Replace("$title$", snippetMap.Title)
                .Replace("$author$", data.Author)
                .Replace("$description$", snippetMap.Description)
                .Replace("$shortcut$", snippetMap.Shortcut)
                .Replace("$data$", $"{ source.Trim() }$end$")
                .Replace("$literals$", literals.TrimEnd())
                ;
            SaveFile(snippetMap.TargetPath, snippet);

            // Compose and save source map of tests.
            sb = new StringBuilder();
            sb.AppendLine(GetFixLength("Snippet:") + snippetMap.ShortTarget);
            sb.AppendLine(GetFixLength("Project:") + snippetMap.Project);
            sb.AppendLine(GetFixLength("FileName:") + snippetMap.FileName);
            sb.AppendLine();

            var phList = new List<string>();
            foreach (var swap in snippetMap.TextSwaps)
                if (!phList.Contains(swap.To))
                {
                    sb.AppendLine(GetFixLength(swap.To + ":") + swap.TestModel);
                    phList.Add(swap.To);
                }

            var testMap = sb.ToString();
            var testMapPath = Path.Combine(
                data.TestBasePath, snippetMap.TargetFolder, Path.GetFileName(snippetMapPath)
                );
            SaveFile(testMapPath, testMap);
        }

        private static SnippetMap GetMap(
            string snippetMapPath,
            BaseData data
            )
        {
            var map = new SnippetMap();
            var targetFolder = "";
            var lines = File.ReadLines(snippetMapPath);
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
                    case "Source":
                        map.SourcePath = value;
                        targetFolder = GetTargetFolder(value);
                        break;
                    case "Target":
                        map.SourcePath = GetSourcePath(map.SourcePath, value, data.TemplateSources);
                        map.TargetFolder = targetFolder;
                        map.ShortTarget = Path.Combine(targetFolder, value);
                        map.TargetPath = Path.Combine(data.TargetBasePath, targetFolder, value);
                        break;
                    case "Title":
                        map.Title = value;
                        break;
                    case "Description":
                        map.Description = value;
                        break;
                    case "Shortcut":
                        map.Shortcut = value;
                        break;
                    case "Test":
                        var tests = value.Split('|');
                        map.Project = tests[0].Trim();
                        map.FileName = tests[1].Trim();
                        break;
                    default:
                        map.TextSwaps.Add(GetSwap(key, value, data.Declarations));
                        break;
                }
            }
            return map;
        }

        private static string GetTargetFolder(
            string sourcePath
            )
        {
            var last = sourcePath.LastIndexOf('\\');
            return sourcePath[..last].Replace('\\', '_');
        }

        private static string GetSourcePath(
            string source,
            string target,
            List<TemplateSource> templateSources
            )
        {
            var templateType = "";
            var sourceBasePath = "";
            var start = target.Substring(0, 2);
            switch (start)
            {
                case "C_":
                    templateType = "Contract";
                    break;
                case "D_":
                    templateType = "Dal";
                    break;
                case "M_":
                    templateType = "WebApi";
                    break;
                default:
                    templateType = "WebApi";
                    break;
            }
            sourceBasePath = templateSources.Find(o => o.TemplateType == templateType)!.SourceBasePath;
            return Path.Combine(sourceBasePath, source);
        }

        private static TextSwap GetSwap(
            string key,
            string value,
            List<Declaration> declarations
            )
        {
            var parts = value.Split('|');
            var id = parts[0].Trim();
            var testModel = parts[1].Trim();

            var declaration = declarations.Find(o => o.ID == id)!;
            var swap = new TextSwap
            {
                To = id,
                ToolTip = declaration.ToolTip,
                DefaultValue = declaration.DefaultValue,
                TestModel = testModel
            };
            if (key.StartsWith("///"))
            {
                swap.From = key.Substring(3).Trim();
                swap.InComment = true;
            }
            else
            {
                swap.From = key;
                swap.InComment = false;
            }
            return swap;
        }

        private static List<string> GetSource(
            string sourcePath
            )
        {
            var source = new List<string>();
            var lines = File.ReadLines(sourcePath);
            var isContent = false;
            foreach (var line in lines)
            {
                if (line.StartsWith('{'))
                {
                    isContent = true;
                    continue;
                }
                if (line.StartsWith('}'))
                {
                    isContent = false;
                    continue;
                }
                if (isContent)
                {
                    source.Add(line);
                }
            }
            return source;
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

        private static string GetFixLength(
            string text
            )
        {
            var length = Math.Max(text.Length + 1, 16);
            return (text + new string(' ', 20)).Substring(0, length);
        }
    }
}
