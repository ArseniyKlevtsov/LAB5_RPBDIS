namespace LAB5_RPBDIS.Models
{
    public class TrainStaff
    {
        public int TrainStaffId { get; set; }
        public int DayOfWeek { get; set; }

        public int TrainId { get; set; }
        public Train? Train { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}
