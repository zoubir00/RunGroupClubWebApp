using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Club> clubs { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
