using BrazilCities.Application.Services.Interfaces;
using BrazilCities.Domain.Requests.State;
using Microsoft.AspNetCore.Mvc;

namespace BrazilCities.Api.Controllers;

[Route("api/[controller]")]
public class StatesController(ILogger<StatesController> logger, IStateService stateService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var states = await stateService.GetAllAsync(cancellationToken);
        return Ok(states);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var state = await stateService.GetByIdAsync(id, cancellationToken);
        
        if (state is null)
        {
            return NoContent();
        }
        
        return Ok(state);
    }
    
    [HttpGet("acronym/{acronym}")]
    public async Task<IActionResult> Get(string acronym, CancellationToken cancellationToken)
    {
        var state = await stateService.GetByAcronymAsync(acronym, cancellationToken);
        
        if (state is null)
        {
            return NoContent();
        }
        
        return Ok(state);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StatePostRequest statePostRequest, CancellationToken cancellationToken)
    {
        var state = await stateService.CreateAsync(statePostRequest, cancellationToken);
        
        return Created($"/api/states/{state.Id}", state);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] StatePutRequest statePutRequest, CancellationToken cancellationToken)
    {
        var result = await stateService.UpdateAsync(id, statePutRequest, cancellationToken);
        
        if (result)
        {
            return Ok();
        }
        
        return BadRequest();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await stateService.DeleteAsync(id, cancellationToken);
        
        if (result)
        {
            return Ok();
        }
        
        return BadRequest();
    }
}