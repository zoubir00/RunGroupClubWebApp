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
    }
}
