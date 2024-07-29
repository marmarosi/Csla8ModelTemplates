using Csla8RestApi.SnippetGenerator.Models;
using System.Reflection;
using System.Text;

namespace Csla8RestApi.SnippetGenerator
{
    internal static class DocsData
    {
        public static void Generate(
            BaseData data
            )
        {
            Console.WriteLine("SnippetData.js");

            var sb = new StringBuilder();
            ComposeCategories(sb, data.Summary);

            var filePath = Path.Combine(data.TargetBasePath, "..\\SnippetData.js");
            File.WriteAllText(filePath, sb.ToString());
        }

        private static void ComposeCategories(
            StringBuilder sb,
            List<Category> data
            )
        {
            sb.AppendLine("const data = [");
            if (data.Count > 0) 
                sb.AppendLine("  {");

            var i = 0;
            foreach (var category in data)
            {
                sb.AppendLine($"    category: '{category.CategoryName}',");
                sb.AppendLine("    models: [{");

                ComposeModels(sb, category.Models);

                if (++i == data.Count)
                    sb.AppendLine("  }");
                else
                    sb.AppendLine("  }, {");
            }

            sb.AppendLine("];");
            sb.AppendLine("export default data;");
        }

        private static void ComposeModels(
            StringBuilder sb,
            List<Model> data
            )
        {
            var i = 0;
            foreach (var model in data)
            {
                sb.AppendLine($"      name: '{model.ModelName}',");
                sb.AppendLine($"      code: '{model.ModelCode}',");
                sb.AppendLine("      snippets: [{");

                ComposeSnippets(sb, model.Snippets);

                if (++i == data.Count)
                    sb.AppendLine("    }]");
                else
                    sb.AppendLine("    }, {");
            }
        }

        private static void ComposeSnippets(
            StringBuilder sb,
            List<SnippetBrief> data
            )
        {
            var i = 0;
            foreach (var snippet in data)
            {
                sb.AppendLine($"        title: '{snippet.Title}',");
                sb.AppendLine($"        shortcut: '{snippet.Shortcut}',");
                sb.AppendLine($"        fileName: '{snippet.FileName}',");
                sb.AppendLine($"        rootName: '{GetX(snippet.RootName)}',");
                sb.AppendLine($"        rootModel: '{GetX(snippet.RootModel)}',");
                sb.AppendLine($"        rootVariable: '{GetX(snippet.RootVariable)}',");
                sb.AppendLine($"        childName: '{GetX(snippet.ChildName)}',");
                sb.AppendLine($"        childModel: '{GetX(snippet.ChildModel)}',");
                sb.AppendLine($"        childVariable: '{GetX(snippet.ChildVariable)}',");
                sb.AppendLine($"        commandName: '{GetX(snippet.CommandName)}',");
                sb.AppendLine($"        commandModel: '{GetX(snippet.CommandModel)}',");
                sb.AppendLine($"        dbContext: '{GetX(snippet.DbContext)}'");

                if (++i == data.Count)
                    sb.AppendLine("      }]");
                else
                    sb.AppendLine("      }, {");
            }
        }
 
        private static string GetX(
            bool paramIsUsed
            )
        {
            return paramIsUsed ? "x" : "";
        }
    }
}
