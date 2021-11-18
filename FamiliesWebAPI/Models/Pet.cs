using System.Text.Json.Serialization;

namespace FamiliesWebAPI.Models {
public class Pet {
    public int Id { get; set; }
    public string Species { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    [JsonIgnore]
    public Family Family { get; set; }
}
}