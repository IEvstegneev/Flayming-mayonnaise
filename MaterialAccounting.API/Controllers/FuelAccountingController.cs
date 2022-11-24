using FuelAccounting.DataAccess;
using MaterialAccounting.API.Contracts;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace FuelAccounting.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class FuelAccountingController : ControllerBase
{
    private readonly ILogger<FuelAccountingController> _logger;
    private readonly IRepository<Refuel> _repository;
    private readonly int _userId;

    public FuelAccountingController(ILogger<FuelAccountingController> logger, IRepository<Refuel> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(RefuelDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRefuels()
    {
        var refuels = await _repository.GetAllAsync();
        var refuelDtos = refuels.Select(r => new RefuelDTO(r));
        return Ok(refuelDtos);
    }

    [HttpGet("refuel/{id}")]
    [ProducesResponseType(typeof(RefuelDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRefuel([FromBody] Guid id)
    {
        var refuel = await _repository.FindAsync(id);
        if (refuel is null)
        {
            return NotFound(id);
        }
        var dto = new RefuelDTO(refuel); // данные по одной заправке 
        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RefuelDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateRefuel([FromBody] CreateRefuelRequest request)
    {
        var newRefuel = new Refuel
        {
            Id = Guid.NewGuid(),
            FromAccount = _userId,
            FromTankId = request.TankId, //Validate by _userId
            EquipmentId = request.EquipmentId, //Validate by _userId
            Date = request.Date,
            Value = request.Value,
            FuelType = FuelType.Diesel, //To do: define by TankId
            ToAccount = _userId, //To do: define by EquipmentId
            //ToTankId - is not actual for a Refuel
        };

        var result = await _repository.AddAsync(newRefuel);

        return CreatedAtAction(nameof(GetRefuel), new { id = result.Id }, new RefuelDTO(result));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateRefuel(Guid id, [FromBody] CreateRefuelRequest request)
    {
        var refuel = await _repository.FindAsync(id);

        if (refuel is null)
        {
            return NotFound(id);
        }

        //??????????????????????

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRefuel(Guid id)
    {
        var refuel = await _repository.FindAsync(id);

        if (refuel is null)
        {
            return NotFound(id);
        }

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}