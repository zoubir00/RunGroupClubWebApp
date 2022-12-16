using System.ComponentModel.DataAnnotations;

namespace RunGroupClubWebApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public String? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

    }
}
