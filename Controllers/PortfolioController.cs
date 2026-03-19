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
    public async Task<IActionResult> GetPortfolio()
    {
        var result = await _service.GetPortfolioAsync();
        return Ok(result);
    }

    [HttpPost("add-stock")]
    public async Task<IActionResult> AddStock(CreateStockRequestDto request)
    {
        await _service.AddStockAsync(request);
        return Ok("Stock added successfully");
    }
}