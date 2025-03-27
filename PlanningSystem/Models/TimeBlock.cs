namespace PlanningSystem.Models;

public class TimeBlock
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int ShiftTypeId { get; set; }
    public int FunctionTypeId { get; set; }
    public ShiftType ShiftType { get; set; }
    public FunctionType FunctionType { get; set; }
} 