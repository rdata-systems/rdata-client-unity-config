using RData.LitJson;

namespace RData.Config
{
    public class RDataConfig<TData>
        : RDataBaseConfig
    {
        [JsonAlias("data")]
        public TData Data { get; set; }
    }
}
