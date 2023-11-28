namespace LAB5_RPBDIS.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int DayOfWeek { get; set; }

        public int TrainId { get; set; }
        public Train? Train { get; set; }

        public int StopId { get; set; }
        public Stop? Stop { get; set; }

        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? DepartureTime { get; set; }

    }
}
