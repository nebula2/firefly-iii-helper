using System;
using System.Collections.Generic;

namespace JfFireflyWebApp.Shared
{
    public class TaxmanResult
    {
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public List<TaxmanMapping> Mappings { get; set; }

        public List<TaxmanTagSearchResult> SearchResults { get; set; } = new List<TaxmanTagSearchResult>();
    }
}
