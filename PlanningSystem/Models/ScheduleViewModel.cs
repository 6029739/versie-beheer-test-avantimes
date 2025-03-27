using System;
using System.Collections.Generic;

namespace PlanningSystem.Models
{
    public class ScheduleViewModel
    {
        public DateTime SelectedDate { get; set; }
        public List<Employee> RestaurantEmployees { get; set; }
        public List<Employee> BarEmployees { get; set; }
        public List<TimeSpan> TimeSlots { get; set; }

        public ScheduleViewModel()
        {
            RestaurantEmployees = new List<Employee>();
            BarEmployees = new List<Employee>();
            TimeSlots = new List<TimeSpan>();
        }
    }
} 