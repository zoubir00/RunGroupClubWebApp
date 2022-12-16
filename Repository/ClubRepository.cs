using Microsoft.EntityFrameworkCore;
using RunGroupClubWebApp.Data;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _db;

        public ClubRepository(ApplicationDbContext Db)
        {
            _db = Db;
        }
        public bool Add(Club club)
        {
            _db.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            _db.Remove(club);
            return Save();
        }

        public async Task<Club> GetClubByIdAsync(int id)
        {
           return await _db.Clubs.Include(a => a.Address).FirstOrDefaultAsync(i => i.Id == id);
             
        }
        public async Task<Club> GetClubByIdAsyncNoTraking(int id)
        {
            return await _db.Clubs.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

        }

        public async Task<IEnumerable<Club>> GetClubs()
        {
            return await _db.Clubs.ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetClubsByCity(string city)
        {
            return await _db.Clubs.Where(c=>c.Address.City.Contains(city)).ToListAsync() ;
        }
        public bool Update(Club club)
        {
            _db.Update(club);
            return Save();
        }

        public bool Save()
        {
            var saved=_db.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
