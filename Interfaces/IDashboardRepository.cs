using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        public Task<List<Club>> GetAllUserClubs();
    }
}
