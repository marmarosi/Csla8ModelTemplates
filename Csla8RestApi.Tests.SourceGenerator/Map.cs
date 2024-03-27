namespace Csla8RestApi.Tests.SourceGenerator
{
    internal class Map
    {
        public string SnippetPath { get; set; }
        public string ShortSource { get; set; }
        public string SourcePath { get; set; }
        public List<Model> Models { get; set; }

        public Map()
        {
            Models = [];
        }
    }

    internal class Model
    {
        public string Placeholder { get; set; }
        public string Name { get; set; }
    }
}
