using PortfolioApi.Models;

namespace PortfolioApi.Repositories
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetPortfolioAsync();
        Task AddStockAsync(Stock stock);

        Task<Stock?> GetBySymbolAsync(string symbol);

        Task UpdateStockAsync(Stock stock);

    }
}