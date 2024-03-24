using System.Text;

namespace Csla8RestApi.SnippetGenerator
{
    internal static class Snippet
    {
        public static void Generate(
            string mapPath,
            string author,
            SnippetResource resource,
            List<Declaration> declarations
            )
        {
            var snippetTemplate = File.ReadAllText("Snippet.xml");
            var literalTemplate = File.ReadAllText("Literal.xml");

            var map = GetMap(mapPath, resource, declarations);
            Console.WriteLine(map.ShortTarget);

            var sb = new StringBuilder();
            foreach (var swap in map.Swaps)
                sb.Append(literalTemplate
                    .Replace("$id$", swap.To)
                    .Replace("$tooltip$", swap.ToolTip)
                    .Replace("$default$", swap.DefaultValue)
                    );
            var literals = sb.ToString();

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
                        line = line.Replace(swap.From, swap.To);
                    }
                }
            var source = string.Join("\r\n", sourceLines);

            var snippet = snippetTemplate
                .Replace("$title$", map.Title)
                .Replace("$author$", author)
                .Replace("$description$", map.Description)
                .Replace("$shortcut$", map.Shortcut)
                .Replace("$data$", source.Trim())
                .Replace("$literals$", literals.TrimEnd())
                ;
            SaveSnippet(map.Target, snippet);
        }

        private static Map GetMap(
            string mapPath,
            SnippetResource resource,
            List<Declaration> declarations
            )
        {
            var map = new Map();
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
                        map.Source = Path.Combine(resource.SourceBasePath, value);
                        break;
                    case "Target":
                        map.ShortTarget = value;
                        map.Target = Path.Combine(resource.TargetBasePath, value);
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
                    default:
                        var declaration = declarations.FirstOrDefault(o => o.ID == value);
                        var swap = new Swap
                        {
                            To = declaration.ID,
                            ToolTip = declaration.ToolTip,
                            DefaultValue = declaration.DefaultValue
                        };
                        if (key.StartsWith("///"))
                        {
                            swap.From = key.Substring(0, 3).Trim();
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

        private static void SaveSnippet(
            string targetPath,
            string content
            )
        {
            CheckFolder(Path.GetDirectoryName(targetPath));
            File.WriteAllText(targetPath, content);
        }

        private static void CheckFolder(
            string folderPath
            )
        {
            if (!Directory.Exists(folderPath))
            {
                CheckFolder(Directory.GetParent(folderPath).FullName);
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}
