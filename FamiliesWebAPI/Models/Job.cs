using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamiliesWebAPI.Models
{
    public class Job
    {
        [Key]
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        
        //public Adult Adult { get; set; }

    }
}