using System.Collections.Generic;

namespace JfFireflyWebApp.Shared
{
    public class TaxmanTagSearchResult
    {
        public TaxmanMapping MappingToSearch { get; set; }
        public string Note { get; set; }
        public float Amount { get; set; } = 0f;

        public List<TagResult> TagSubResults { get; set; } = new List<TagResult>();

        public class TagResult
        {
            public string Tag { get; set; }
            public int TagId { get; set; }
            public float Subvalue { get; set; }
            public string FireflyUrl { get; set; }
        }
    }
}
