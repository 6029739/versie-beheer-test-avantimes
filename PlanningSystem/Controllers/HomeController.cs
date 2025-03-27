using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlanningSystem.Models;

namespace PlanningSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly List<Employee> _dummyEmployees;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;

        // Initialize dummy employees
        _dummyEmployees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Theo Rossen", Department = "Restaurant", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 30, 0) },
            new Employee { Id = 2, Name = "Peter de Hermsen", Department = "Restaurant", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 30, 0) },
            new Employee { Id = 3, Name = "Stefan Kroon", Department = "Restaurant", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 30, 0) },
            new Employee { Id = 4, Name = "Kelly Slöbber", Department = "Restaurant", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 30, 0) },
            new Employee { Id = 5, Name = "Jan Medewerker", Department = "Restaurant", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(13, 0, 0) },
            new Employee { Id = 6, Name = "Piet Medewerker", Department = "Bar", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 30, 0) },
            new Employee { Id = 7, Name = "Klaas Medewerker", Department = "Bar", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 30, 0) }
        };
    }

    public IActionResult Index([FromQuery] string? date = null)
    {
        var selectedDate = DateTime.Now;
        if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out DateTime parsedDate))
        {
            selectedDate = parsedDate;
        }

        var timeSlots = new List<TimeSpan>();
        
        // Generate time slots from 00:00 to 23:00
        for (int hour = 0; hour < 24; hour++)
        {
            timeSlots.Add(new TimeSpan(hour, 0, 0));
        }

        var viewModel = new ScheduleViewModel
        {
            SelectedDate = selectedDate,
            RestaurantEmployees = _dummyEmployees.FindAll(e => e.Department == "Restaurant"),
            BarEmployees = _dummyEmployees.FindAll(e => e.Department == "Bar"),
            TimeSlots = timeSlots
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
