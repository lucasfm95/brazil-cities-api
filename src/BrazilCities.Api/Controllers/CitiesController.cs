using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Domain.Requests.City;
using Microsoft.AspNetCore.Mvc;

namespace BrazilCities.Api.Controllers;

[Route("api/[controller]")]
public class CitiesController(ILogger<CitiesController> logger, ICityService cityService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var cities = await cityService.GetAllAsync(cancellationToken);
        return Ok(cities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var city = await cityService.GetByIdAsync(id, cancellationToken);

        if (city is null)
        {
            return NoContent();
        }
        
        return Ok(city);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CityPostRequest cityPostRequest, CancellationToken cancellationToken)
    {
        var city = await cityService.CreateAsync(cityPostRequest, cancellationToken);
        
        return Created($"/api/cities/{city.Id}", city);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CityPutRequest cityPutRequest, CancellationToken cancellationToken)
    {
        var result = await cityService.UpdateAsync(cityPutRequest, cancellationToken);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await cityService.DeleteAsync(id, cancellationToken);
        
        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }
}