using PortfolioApi.Data;
using PortfolioApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PortfolioApi.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PortfolioDbContext _context;

        public PortfolioRepository(PortfolioDbContext context)
        {
            _context = context;

        }

        public async Task<List<Stock>> GetPortfolioAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);

            await _context.SaveChangesAsync();
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);

        }
        
        public async Task UpdateStockAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }
    }
}