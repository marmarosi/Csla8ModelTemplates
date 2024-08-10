namespace Csla8RestApi.SnippetGenerator.Models
{
    internal class SnippetBrief
    {
        public string Title { get; set; }
        public string Shortcut { get; set; }
        public string FileName { get; set; }
        public bool RootName { get; set; }
        public bool RootModel { get; set; }
        public bool RootVariable { get; set; }
        public bool ChildName { get; set; }
        public bool ChildModel { get; set; }
        public bool ChildVariable { get; set; }
        public bool CommandName { get; set; }
        public bool CommandModel { get; set; }
        public bool DbContext { get; set; }
    }
}
