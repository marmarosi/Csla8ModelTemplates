namespace Csla8RestApi.SnippetGenerator.Models
{
    internal class BaseData
    {
        public string Author { get; set; }
        public string TargetBasePath { get; set; }
        public string TestBasePath { get; set; }
        public List<Declaration> Declarations { get; set; }
        public List<TemplateSource> TemplateSources { get; set; }
        public string SnippetTemplate { get; set; }
        public string LiteralTemplate { get; set; }
    }
}
