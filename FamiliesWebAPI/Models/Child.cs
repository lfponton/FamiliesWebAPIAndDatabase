using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FamiliesWebAPI.Models {
public class Child : Person {
    
    public IList<Interest> Interests { get; set; }
    public IList<Pet> Pets { get; set; }
    [JsonIgnore]
    public Family Family { get; set; }
}
}