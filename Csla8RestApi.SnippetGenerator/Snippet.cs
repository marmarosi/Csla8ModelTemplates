using System.Text;

namespace Csla8RestApi.SnippetGenerator
{
    internal static class Snippet
    {
        public static void Generate(
            BaseData data,
            string mapPath,
            SnippetResource resource,
            List<Declaration> declarations
            )
        {
            // Get templates.
            var snippetTemplate = File.ReadAllText("Snippet.xml");
            var literalTemplate = File.ReadAllText("Literal.xml");

            // Get mapping data to convert source to snippet.
            var map = GetMap(mapPath, data.TargetBasePath, resource, declarations);
            Console.WriteLine(map.ShortTarget);

            // Compose literals.
            var sb = new StringBuilder();
            foreach (var swap in map.Swaps)
                sb.Append(literalTemplate
                    .Replace("$id$", swap.To)
                    .Replace("$tooltip$", swap.ToolTip)
                    .Replace("$default$", swap.DefaultValue)
                    );
            var literals = sb.ToString();

            // Read source.
            var sourceLines = GetSource(map.Source);
            foreach (var swap in map.Swaps)
                for (int i = 0; i < sourceLines.Count; i++)
                {
                    var line = sourceLines[i];
                    var isComment = line.TrimStart().StartsWith("///");
                    if (swap.InComment && isComment ||
                        !swap.InComment && !isComment
                        )
                    {
                        sourceLines[i] = line.Replace(swap.From, $"${ swap.To }$");
                    }
                }
            var source = string.Join("\r\n", sourceLines);

            // Compose and save snippet.
            var snippet = snippetTemplate
                .Replace("$title$", map.Title)
                .Replace("$author$", data.Author)
                .Replace("$description$", map.Description)
                .Replace("$shortcut$", map.Shortcut)
                .Replace("$data$", $"{ source.Trim() }$end$")
                .Replace("$literals$", literals.TrimEnd())
                ;
            SaveFile(map.Target, snippet);

            // Compose and save source map of tests.
            sb = new StringBuilder();
            sb.AppendLine(GetFixLength("Snippet:") + map.ShortTarget);
            sb.AppendLine(GetFixLength("Project:") + map.Project);
            sb.AppendLine(GetFixLength("FileName:") + map.FileName);
            sb.AppendLine();
            foreach (var swap in map.Swaps)
                sb.AppendLine(GetFixLength(swap.To + ":") + swap.TestModel);

            var testMap = sb.ToString();
            var testMapPath = Path.Combine(
                data.TestBasePath, map.TargetFolder, Path.GetFileName(mapPath)
                );
            SaveFile(testMapPath, testMap);
        }

        private static Map GetMap(
            string mapPath,
            string targetBasePath,
            SnippetResource resource,
            List<Declaration> declarations
            )
        {
            var map = new Map();
            var targetFolder = "";
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
                    case "Source":
                        targetFolder = GetTargetFolder(value);
                        map.Source = Path.Combine(resource.SourceBasePath, value);
                        break;
                    case "Target":
                        map.TargetFolder = targetFolder;
                        map.ShortTarget = Path.Combine(targetFolder, value);
                        map.Target = Path.Combine(targetBasePath, targetFolder, value);
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
                        var parts = value.Split('|');
                        var id = parts[0].Trim();
                        var testModel = parts[1].Trim();

                        var declaration = declarations.FirstOrDefault(o => o.ID == id);
                        var swap = new Swap
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
                        map.Swaps.Add(swap);
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
