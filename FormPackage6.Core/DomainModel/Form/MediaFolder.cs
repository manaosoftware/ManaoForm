using Newtonsoft.Json;

namespace FormPackage6.Core.DomainModel.Form
{
    public class MediaFolder
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
