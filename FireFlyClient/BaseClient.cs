namespace FireflyIII
{
    public partial class BaseClient
    {
        public FireflyClientConfig Config { get; set; }

        public BaseClient(FireflyClientConfig config)
        {
            Config = config;
        }
    }
}
