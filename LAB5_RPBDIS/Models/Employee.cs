namespace LAB5_RPBDIS.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int? age { get; set; }
        public float? WorkExperience { get; set; }

        public int PositionID { get; set; }
        public Position? Position { get; set; }

        public List<Train> Trains { get; set; } = new();
        public List<TrainStaff> TrainStaffs { get; set; } = new();
    }
}
