using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroupClubWebApp.Models
{
    public class AppUser:IdentityUser
    {

        public int Pace { get; set; }
        public int MileAge { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }

        public Address? Adress { get; set; }

        public ICollection<Club>? clubs { get; set; }

        public ICollection<Race>? races { get; set; }
    }
}
