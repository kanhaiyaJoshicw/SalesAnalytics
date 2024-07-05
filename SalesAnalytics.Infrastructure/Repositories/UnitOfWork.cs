using SalesAnalytics.Core.Interfaces;
using SalesAnalytics.Core;
using System.Threading.Tasks;

namespace SalesAnalytics.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesDbContext _context;

        public UnitOfWork(SalesDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}
