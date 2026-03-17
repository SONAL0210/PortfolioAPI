namespace PortfolioApi.Shared.Responses;

public class ErrorResponse
{
    public string Error { get; set; }

    public string Message { get; set; }
    
    public int StatusCode { get; set; }
}