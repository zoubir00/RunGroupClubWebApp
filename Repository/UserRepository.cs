using Microsoft.EntityFrameworkCore;
using RunGroupClubWebApp.Data;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Repository
{
    public class UserRepository : IUserRespository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(AppUser appUser)
        {
            _dbContext.Users.Add(appUser);
            return Save();
        }

        public bool Delete(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser appUser)
        {
            _dbContext.Users.Update(appUser);
            return Save();
        }
    }
}
