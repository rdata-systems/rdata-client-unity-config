using RData.LitJson;

namespace RData.Config.Examples.ConfigClientExample
{
    public class TestConfig : RDataConfig<TestConfig.TestConfigData>
    {
        public class TestConfigData
        {
            [JsonAlias("test")]
            public string Test { get; set; }
        }
    }
}
