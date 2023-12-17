using RailwayTrafficSolution.Models;
using RailwayTrafficSolution.ViewModels.EmployeeViewModels;

namespace RailwayTrafficSolution.ViewModels.ScheduleViewModels
{
    public class ScheduleIndexViewModel
    {
        public IEnumerable<Schedule> Schedules { get; }
        public PageViewModel PageViewModel { get; }
        public ScheduleFilterViewModel FilterViewModel { get; }
        public ScheduleSortViewModel SortViewModel { get; }
        public ScheduleIndexViewModel(IEnumerable<Schedule> schedules, PageViewModel pageViewModel,
            ScheduleFilterViewModel filterViewModel, ScheduleSortViewModel sortViewModel)
        {
            Schedules = schedules;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}
