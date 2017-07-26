using RData.LitJson;

namespace RData.Config
{
    public class RDataBaseConfig
    {
        [JsonAlias("id")]
        public string Id { get; set; }

        [JsonAlias("name")]
        public string Name { get; set; }

        [JsonAlias("configVersion")]
        public int ConfigVersion { get; set; }
    }
}
