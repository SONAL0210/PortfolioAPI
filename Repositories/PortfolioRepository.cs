using PortfolioApi.Models;

namespace PortfolioApi.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private static Dictionary<string , Stock> _stocks= new();

        public List<Stock> GetPortfolio()
        {
            return _stocks.Values.ToList();
        }

        public void AddStock(Stock stock)
        {
            if(_stocks.ContainsKey(stock.Symbol))
            {
                _stocks[stock.Symbol].Quantity += stock.Quantity;

            }
            else
            {
                _stocks.Add(stock.Symbol,stock);
            }
            

        }
    }
}