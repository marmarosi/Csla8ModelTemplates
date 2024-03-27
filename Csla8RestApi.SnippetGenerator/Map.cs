namespace Csla8RestApi.SnippetGenerator
{
    internal class Map
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string TargetFolder { get; set; }
        public string ShortTarget { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Shortcut { get; set; }
        public string Project { get; set; }
        public string FileName { get; set; }
        public List<Swap> Swaps { get; set; }

        public Map()
        {
            Swaps = [];
        }
    }

    internal class Swap
    {
        public string From { get; set; }
        public string To { get; set; }
        public bool InComment { get; set; }
        public string ToolTip { get; set; }
        public string DefaultValue { get; set; }
        public string TestModel { get; set; }
    }
}
