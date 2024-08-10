namespace Csla8RestApi.Tests.TestGenerator.Models
{
    internal class TestMap
    {
        public string SnippetPath { get; set; }
        public string TestPath { get; set; }
        public string ShortTest { get; set; }
        public string ShortWrapper { get; set; }
        public List<TextSwap> TextSwaps { get; set; }

        public TestMap()
        {
            TextSwaps = [];
        }
    }
}
