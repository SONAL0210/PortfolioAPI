using PortfolioApi.Repositories;
using PortfolioApi.Models;
using System.Linq;
using PortfolioApi.DTOs;
using PortfolioApi.Shared.Exceptions;

namespace PortfolioApi.Services
{
    public class PortfolioService
    {
        private readonly IPortfolioRepository _repository;

        public PortfolioService(IPortfolioRepository repository)
        {
            _repository = repository;
        }

        public async Task<PortfolioResponseDto> GetPortfolioAsync()
        {
            var stocks = await _repository.GetPortfolioAsync();

            var stockDtos = stocks.Select(s => new StockDto
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

        public async Task AddStockAsync(CreateStockRequestDto request)
        {
            // Validation
            if (request.Quantity <= 0)
                throw new BadRequestException("Quantity must be greater than 0");

            if (string.IsNullOrWhiteSpace(request.Symbol))
                throw new BadRequestException("Symbol is required");

            var symbol = request.Symbol.ToUpper();

            var existing = await _repository.GetBySymbolAsync(symbol);

            if (existing != null)
            {
                existing.Quantity += request.Quantity;
                await _repository.UpdateStockAsync(existing);
            }
            else
            {
                var stock = new Stock
                {
                    Symbol = symbol,
                    Quantity = request.Quantity,
                    BuyPrice = request.BuyPrice
                };

                await _repository.AddStockAsync(stock);
            }
        }
    }
}