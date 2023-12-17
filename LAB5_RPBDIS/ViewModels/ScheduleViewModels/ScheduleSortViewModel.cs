using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.ScheduleViewModels
{
    public class ScheduleSortViewModel
    {
        public SortState DayOfWeekSort { get; }
        public SortState ArrivalTimeSort { get; }
        public SortState DepartureTimeSort { get; }
        public SortState Current { get; }

        public ScheduleSortViewModel(SortState sortOrder)
        {
            DayOfWeekSort = sortOrder == SortState.DayOfWeekAsc ? SortState.DayOfWeekDesc : SortState.DayOfWeekAsc;
            ArrivalTimeSort = sortOrder == SortState.ArriveTimeAsc ? SortState.ArriveTimeDesc : SortState.ArriveTimeAsc;
            DepartureTimeSort = sortOrder == SortState.DepartureTimeAsc ? SortState.DepartureTimeDesc : SortState.DepartureTimeAsc;
            Current = sortOrder;
        }
    }
}
