namespace Csla8RestApi.SnippetGenerator
{
    internal class Map
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string ShortTarget { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Shortcut { get; set; }
        public List<Swap> Swaps { get; set; }

        public Map()
        {
            Swaps = new List<Swap>();
        }
    }

    internal class Swap
    {
        public string From { get; set; }
        public string To { get; set; }
        public bool InComment { get; set; }
        public string ToolTip { get; set; }
        public string DefaultValue { get; set; }
    }
}
