namespace LAB5_RPBDIS.Models
{
    public class Stop
    {
        public int StopId { get; set; }
        public string? Name { get; set; }
        public bool? IsRailwayStation { get; set; }
        public bool? HasWaitingRoom { get; set; }

        public List<Train> Trains { get; set; } = new();
        public List<Schedule> Schedules { get; set; } = new();

    }
}
