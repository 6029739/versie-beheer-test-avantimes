using Microsoft.AspNetCore.Mvc;
using PlanningSystem.Models;

namespace PlanningSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private static List<ShiftType> _services = new List<ShiftType>();
    private static int _nextId = 1;

    [HttpGet]
    public IActionResult GetServices()
    {
        return Ok(_services);
    }

    [HttpPost]
    public IActionResult CreateService([FromBody] ShiftType service)
    {
        service.Id = _nextId++;
        _services.Add(service);
        return Ok(service);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateService(int id, [FromBody] ShiftType service)
    {
        var existingService = _services.FirstOrDefault(s => s.Id == id);
        if (existingService == null)
        {
            return NotFound();
        }

        existingService.Name = service.Name;
        existingService.Icon = service.Icon;
        existingService.Color = service.Color;

        return Ok(existingService);
    }
} 