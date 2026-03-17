using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Services;
using PortfolioApi.Models;
using PortfolioApi.DTOs;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly  PortfolioService _service;

    public PortfolioController(PortfolioService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetPortfolio()
    {
        var result = _service.GetPortfolio();

        return Ok(result);
    }

    [HttpPost("add-stock")]
    public IActionResult AddStock(CreateStockRequestDto request)
    {
        try
        {
            _service.AddStock(request);
            return Ok("Stock added successfully");
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }    
    }
    
}