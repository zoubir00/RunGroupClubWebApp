using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Interfaces
{
    public interface IUserRespository
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUserById(string id);
        bool Add(AppUser appUser);
        bool Update(AppUser appUser);
        bool Delete(AppUser appUser);
        bool Save();
    }
}
