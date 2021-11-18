using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamiliesWebAPI.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        [JsonIgnore]
        public Adult Adult { get; set; }

    }
}