namespace PortfolioApi.DTOs
{
    public class StockDto
    {

        public string Symbol { get; set; }

        public int Quantity { get; set; }
        
        public decimal BuyPrice{ get; set;}
    }
}