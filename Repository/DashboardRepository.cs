using Microsoft.EntityFrameworkCore;
using RunGroupClubWebApp.Data;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public DashboardRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _HttpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _HttpContextAccessor.HttpContext.User.GetUserId();
            var userClub = _dbContext.Clubs.Where(r => r.AppUser.Id == curUser);
            return userClub.ToList();
        }



        public async Task<AppUser> GetUserById(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _dbContext.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Update(AppUser user)
        {
            _dbContext.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
