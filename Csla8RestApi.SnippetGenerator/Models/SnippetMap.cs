namespace Csla8RestApi.SnippetGenerator.Models
{
    internal class SnippetMap
    {
        public string SourcePath { get; set; }
        public string Region { get; set; }
        public string TargetPath { get; set; }
        public string TargetFolder { get; set; }
        public string ShortTarget { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Shortcut { get; set; }
        public string Project { get; set; }
        public string FileName { get; set; }
        public List<TextSwap> TextSwaps { get; set; }

        public SnippetMap()
        {
            TextSwaps = [];
        }
    }
}
