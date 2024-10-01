using Microsoft.EntityFrameworkCore;
using GameZone.PL.Models;
using GameZone.PL.Interfaces;

namespace GameZone.PL.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // تنفيذ طريقة GetAllGames التي تشمل الفئات والأجهزة المرتبطة
        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games
                          .Include(g => g.Category)
            .Include(g => g.Devices)
            .ThenInclude(d => d.Device)
            .AsNoTracking()
            .ToList();
        }
    }
}
