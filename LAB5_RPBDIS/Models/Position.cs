namespace LAB5_RPBDIS.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string? PositionName { get; set; }
        public float? SalaryUsd { get; set; }

        public List<Employee> Employees { get; set; } = new();

    }
}
