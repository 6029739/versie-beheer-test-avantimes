using Microsoft.AspNetCore.Mvc;
using PlanningSystem.Models;

namespace PlanningSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FunctionsController : ControllerBase
{
    private static List<FunctionType> _functions = new List<FunctionType>();
    private static int _nextId = 1;

    [HttpGet]
    public IActionResult GetFunctions()
    {
        return Ok(_functions);
    }

    [HttpPost]
    public IActionResult CreateFunction([FromBody] FunctionType function)
    {
        function.Id = _nextId++;
        _functions.Add(function);
        return Ok(function);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFunction(int id, [FromBody] FunctionType function)
    {
        var existingFunction = _functions.FirstOrDefault(f => f.Id == id);
        if (existingFunction == null)
        {
            return NotFound();
        }

        existingFunction.Name = function.Name;
        existingFunction.Icon = function.Icon;
        existingFunction.Color = function.Color;

        return Ok(existingFunction);
    }
} 