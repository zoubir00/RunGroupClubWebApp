using RunGroupClubWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroupClubWebApp.Models
{
    public class Race
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Address")]
        public int AdressId { get; set; }
        public Address? Address { get; set; }
        public RaceCategory RaceCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
