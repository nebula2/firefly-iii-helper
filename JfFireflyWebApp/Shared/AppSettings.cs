using FireflyIII;
using System.Collections.Generic;

namespace JfFireflyWebApp.Shared
{
    public class AppSettings
    {
        public FireflyClientConfig FireflyConfig { get; set; }

        public List<TaxmanMapping> TaxmanMappings { get; set; }
    }
}
