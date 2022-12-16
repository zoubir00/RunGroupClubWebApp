using RunGroupClubWebApp.Data.Enum;
using RunGroupClubWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroupClubWebApp.ViewModel
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public Address? Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
