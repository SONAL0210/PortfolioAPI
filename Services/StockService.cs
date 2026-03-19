using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualBasic;
using PortfolioApi.DTOs;
using PortfolioApi.Repositories;
using PortfolioApi.Shared.Exceptions;
using System.Text.Json;

namespace PortfolioApi.Services;
public class StockService : IStockService
{
    private readonly IDistributedCache _cache;

    private readonly IPortfolioRepository _repository;

    public StockService(IDistributedCache cache, IPortfolioRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }

    public async Task<StockDto> GetStockAsync(string symbol)
    {
        symbol = symbol.ToUpper();
        var cacheKey = $"stock:{symbol}";

        try
        {
            var cachedJson = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedJson))
            {
                var cachedStock = JsonSerializer.Deserialize<StockDto>(cachedJson);
                if (cachedStock is not null)
                    return cachedStock;
            }
        }
        catch (Exception)
        {
            // log error (later we add ILogger)
            // fallback to DB
        }

        var stock = _repository.GetPortfolio().FirstOrDefault(s => s.Symbol == symbol);
        if (stock is null)
            throw new StockNotFoundException(symbol);

        var stockDto = new StockDto
        {
            Symbol = stock.Symbol,
            Quantity = stock.Quantity,
            BuyPrice = stock.BuyPrice
        };

        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            };

            var json = JsonSerializer.Serialize(stockDto);
            await _cache.SetStringAsync(cacheKey, json, options);
        }
        catch (Exception)
        {
            // log error
            // don't fail request
        }

        return stockDto;
    }
}