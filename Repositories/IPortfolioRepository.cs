using PortfolioApi.Models;

namespace PortfolioApi.Repositories
{
    public interface IPortfolioRepository
    {
        List<Stock> GetPortfolio();
        void AddStock(Stock stock);

    }
}