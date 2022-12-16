using Microsoft.EntityFrameworkCore;
using RunGroupClubWebApp.Data;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Repository
{
    public class RaceRepository:IRaceRepository
    {
        private readonly ApplicationDbContext _db;

        public RaceRepository(ApplicationDbContext Db)
        {
            _db = Db;
        }

        public bool Add(Race race)
        {
            _db.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _db.Remove(race);
            return Save();
        }

        public async Task<Race> GetRaceByIdAsync(int id)
        {
            return await _db.Races.Include(a=>a.Address).FirstOrDefaultAsync(i=>i.Id==id);
        }

        public async Task<IEnumerable<Race>> GetRaces()
        {
            return await _db.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetRacesByCity(string city)
        {
            return await _db.Races.Where(c=>c.Address.City==city).ToListAsync();
        }

        public bool Save()
        {
            var saved=_db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            throw new NotImplementedException();
        }
    }
}
