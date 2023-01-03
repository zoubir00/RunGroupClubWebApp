using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        public Task<List<Club>> GetAllUserClubs();
        public Task<AppUser> GetUserById(string id);
        public Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
