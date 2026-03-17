namespace PortfolioApi.Shared.Exceptions;

public class StockNotFoundException : Exception
{
    public StockNotFoundException(string symbol):base($"Stock with sumbol {symbol} not found")
    {

    }

}
