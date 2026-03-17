using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Models
{
    public class Stock
    {
        public string Symbol { get; set; }

        public int Quantity {get; set;}

        public decimal BuyPrice {get; set;}
    }
}