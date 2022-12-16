using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetClubs();
        Task<Club> GetClubByIdAsync(int id);
        Task<Club> GetClubByIdAsyncNoTraking(int id);
        Task<IEnumerable<Club>> GetClubsByCity(string city);
        bool Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();

    }
}
