using System.ComponentModel.DataAnnotations;

namespace LAB5_RPBDIS.Models
{
    public class Stop
    {
        public int StopId { get; set; }

        [Display(Name = "Остановка")]
        public string? Name { get; set; }

        [Display(Name = "Является ЖД станцией?")]
        public bool? IsRailwayStation { get; set; }

        [Display(Name = "Есть команата ожидания?")]
        public bool? HasWaitingRoom { get; set; }

        public List<Train> Trains { get; set; } = new();
        public List<Schedule> Schedules { get; set; } = new();

    }
}
