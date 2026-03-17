using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using PortfolioApi.DTOs;
using PortfolioApi.Repositories;
using PortfolioApi.Shared.Exceptions;

namespace PortfolioApi.Services;
public class StockService : IStockService
{
    private readonly IMemoryCache _cache;

    private readonly IPortfolioRepository _repository;

    public StockService(IMemoryCache cache, IPortfolioRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }

    public async Task<StockDto> GetStockAsync(string symbol)
    {
        symbol = symbol.ToUpper();
        if (_cache.TryGetValue(symbol, out StockDto cached))
        {
            return cached;
        }

        var stocks = _repository.GetPortfolio();
        var stock = stocks.FirstOrDefault(s => s.Symbol == symbol);

        if (stock == null)
        {
            throw new StockNotFoundException(symbol);
        }

        var stockDto = new StockDto
        {
            Symbol = stock.Symbol,
            Quantity = stock.Quantity,
            BuyPrice = stock.BuyPrice
        };

        _cache.Set(symbol, stockDto, TimeSpan.FromSeconds(30));

        return stockDto;
    }
}