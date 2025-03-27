using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanningSystem.Models;

namespace PlanningSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeBlocksController : ControllerBase
{
    private static List<TimeBlock> _timeBlocks = new List<TimeBlock>();
    private static int _nextId = 1;

    [HttpPost]
    public IActionResult CreateTimeBlock([FromBody] TimeBlock timeBlock)
    {
        timeBlock.Id = _nextId++;
        _timeBlocks.Add(timeBlock);
        return Ok(timeBlock);
    }

    [HttpGet]
    public IActionResult GetTimeBlocks([FromQuery] DateTime? date = null)
    {
        var query = _timeBlocks.AsQueryable();

        if (date.HasValue)
        {
            query = query.Where(tb => tb.Date.Date == date.Value.Date);
        }

        return Ok(query.ToList());
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTimeBlock(int id, [FromBody] TimeBlock timeBlock)
    {
        var existingBlock = _timeBlocks.FirstOrDefault(tb => tb.Id == id);
        if (existingBlock == null)
        {
            return NotFound();
        }

        existingBlock.StartTime = timeBlock.StartTime;
        existingBlock.EndTime = timeBlock.EndTime;
        existingBlock.ShiftTypeId = timeBlock.ShiftTypeId;
        existingBlock.FunctionTypeId = timeBlock.FunctionTypeId;

        return Ok(existingBlock);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTimeBlock(int id)
    {
        var block = _timeBlocks.FirstOrDefault(tb => tb.Id == id);
        if (block == null)
        {
            return NotFound();
        }

        _timeBlocks.Remove(block);
        return Ok();
    }
} 