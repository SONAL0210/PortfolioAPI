using PortfolioApi.Repositories;
using PortfolioApi.Models;
using System.Linq;
using PortfolioApi.DTOs;

namespace PortfolioApi.Services
{
    public class PortfolioService
    {
        private readonly IPortfolioRepository _repository;

        public PortfolioService(IPortfolioRepository repository)
        {
            _repository = repository;
        }

        public PortfolioResponseDto GetPortfolio()
        {
            var stocks = _repository.GetPortfolio();

            var stockDtos = stocks.Select(s => 
            new StockDto
            {
                Symbol = s.Symbol,
                Quantity = s.Quantity,
                BuyPrice = s.BuyPrice
            }).ToList();

            return new PortfolioResponseDto
            {
                Stocks = stockDtos,
                TotalValue = stocks.Sum(s => s.Quantity * s.BuyPrice),
                Currency = "INR"
            };
        }

        public void AddStock(CreateStockRequestDto request)
        {
            var stock = new Stock
            {
                Symbol = request.Symbol.ToUpper(),
                Quantity = request.Quantity,
                BuyPrice = request.BuyPrice

            };

            _repository.AddStock(stock);

            /*var stocks = _repository.GetPortfolio();

            var existing = stocks.FirstOrDefault(s=> s.Symbol == stock.Symbol);

            if( existing != null)
            {
                
                existing.Quantity += stock.Quantity;
                
            }
            else
            {
                _repository.AddStock(stock);
            }*/
            
        }
    }
}