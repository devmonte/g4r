using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class User
{
    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}