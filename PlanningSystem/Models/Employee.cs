using System;

namespace PlanningSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; } // "Restaurant" or "Bar"
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
} 