namespace PortfolioApi.DTOs
{
    public class PortfolioResponseDto
    {
        public List<StockDto> Stocks{ get; set;}

        public decimal TotalValue{ get; set;}

        public string Currency{ get; set;}
    }
}