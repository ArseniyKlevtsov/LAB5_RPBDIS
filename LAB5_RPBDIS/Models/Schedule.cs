using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayTrafficSolution.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        [Display(Name = "День недели")]
        public int DayOfWeek { get; set; }

        [Display(Name = "Поезд")]
        public int TrainId { get; set; }

        [Display(Name = "Поезд")]
        [ForeignKey("TrainId")]
        public Train? Train { get; set; }

        [Display(Name = "Остановка")]
        public int StopId { get; set; }

        [Display(Name = "Остановка")]
        [ForeignKey("StopId")]
        public Stop? Stop { get; set; }

        [Display(Name = "Время прибытия")]
        public TimeSpan? ArrivalTime { get; set; }
        [Display(Name = "Время отправки")]
        public TimeSpan? DepartureTime { get; set; }

    }
}
