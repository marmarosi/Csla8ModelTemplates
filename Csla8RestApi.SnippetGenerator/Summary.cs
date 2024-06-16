using Csla8RestApi.SnippetGenerator.Models;
using System.Text;

namespace Csla8RestApi.SnippetGenerator
{
    internal static class Summary
    {
        public static void Generate(
            BaseData data
            )
        {
            Console.WriteLine();
            Console.WriteLine("Snippets.html");

            var mainTemplate = File.ReadAllText(GetAbsolutePath(".\\Templates\\Main.html"));
            var categoryTemplate = File.ReadAllText(GetAbsolutePath(".\\Templates\\Category.html"));
            var modelTemplate = File.ReadAllText(GetAbsolutePath(".\\Templates\\Model.html"));
            var snippetTemplate = File.ReadAllText(GetAbsolutePath(".\\Templates\\Snippet.html"));

            var categories = ComposeCategories(data.Summary, categoryTemplate, modelTemplate, snippetTemplate);
            var contents = mainTemplate.Replace("#contents#", categories);

            var filePath = Path.Combine(data.TargetBasePath, "..\\Snippets.html");
            File.WriteAllText(filePath, contents);
        }

        private static string ComposeCategories(
            List<Category> data,
            string categoryTemplate,
            string modelTemplate,
            string snippetTemplate
            )
        {
            var sb = new StringBuilder();

            foreach (var category in data)
            {
                var models = ComposeModels(category.Models, modelTemplate, snippetTemplate);
                var contents = categoryTemplate
                    .Replace("#name#", category.CategoryName)
                    .Replace("#models#", models);
                sb.Append(contents);
            }
            return sb.ToString();
        }

        private static string ComposeModels(
            List<Model> data,
            string modelTemplate,
            string snippetTemplate
            )
        {
            var sb = new StringBuilder();

            foreach (var model in data)
            {
                var snippets = ComposeSnippets(model.Snippets, snippetTemplate);
                var contents = modelTemplate
                    .Replace("#code#", model.ModelCode)
                    .Replace("#name#", model.ModelName)
                    .Replace("#snippets#", snippets);
                sb.Append(contents);
            }
            return sb.ToString();
        }

        private static string ComposeSnippets(
            List<SnippetBrief> data,
            string snippetTemplate
            )
        {
            var sb = new StringBuilder();

            foreach (var snippet in data)
            {
                var contents = snippetTemplate
                    .Replace("#title#", snippet.Title)
                    .Replace("#shortcut#", snippet.Shortcut)
                    .Replace("#fileName#", snippet.FileName)
                    .Replace("#rootName#", snippet.RootName ? "x" : "")
                    .Replace("#rootModel#", snippet.RootModel ? "x" : "")
                    .Replace("#rootVariable#", snippet.RootVariable ? "x" : "")
                    .Replace("#childName#", snippet.ChildName ? "x" : "")
                    .Replace("#childModel#", snippet.ChildModel ? "x" : "")
                    .Replace("#childVariable#", snippet.ChildVariable ? "x" : "")
                    .Replace("#commandName#", snippet.CommandName ? "x" : "")
                    .Replace("#commandModel#", snippet.CommandModel ? "x" : "")
                    .Replace("#dbContext#", snippet.DbContext ? "x" : "")
                    ;
                sb.Append(contents);
            }
            return sb.ToString();
        }

        private static string GetAbsolutePath(
            string? path
            )
        {
            if (string.IsNullOrWhiteSpace(path))
                path = Directory.GetCurrentDirectory();
            if (path.StartsWith('.'))
                path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\", path);
            return path;
        }
    }
}
