using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Models.StaticData;

namespace FamiliesWebAPI.Models {
    public class Adult : Person {
        [ForeignKey("JobId")]
        public Job Job { get; set; }
        
        [JsonIgnore]
        public Family Family { get; set; }

        public Adult()
        {
            Job = new Job();
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}