using RailwayTrafficSolution.Data;
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
        [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$", ErrorMessage = "Неверный формат времени. Используйте формат hh:mm:ss.")]
        public TimeSpan? ArrivalTime { get; set; }
        [Display(Name = "Время отправки")]
        [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$", ErrorMessage = "Неверный формат времени. Используйте формат hh:mm:ss.")]
        public TimeSpan? DepartureTime { get; set; }

    }
}
