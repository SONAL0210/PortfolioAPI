using PortfolioApi.DTOs;

public interface IStockService
{
    Task<StockDto> GetStockAsync(string symbol);
}