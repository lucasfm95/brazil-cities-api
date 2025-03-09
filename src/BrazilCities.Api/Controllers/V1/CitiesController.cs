using Asp.Versioning;
using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Domain.Requests.City;
using Microsoft.AspNetCore.Mvc;

namespace BrazilCities.Api.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CitiesController(ICityService cityService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryParametersCity queryParametersCity, CancellationToken cancellationToken)
    {
        return Ok(await cityService.GetAllAsync(queryParametersCity, cancellationToken));
    }

    [HttpGet("{id:int}")]
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

        return Created($"/api/cities/{city?.Id}", city);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] CityPutRequest cityPutRequest, CancellationToken cancellationToken)
    {
        var result = await cityService.UpdateAsync(cityPutRequest, cancellationToken);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete("{id:int}")]
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