using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.TrainStuffViewModels
{
    public class TrainStuffSortViewModel
    {
        public SortState DayOfWeekSort { get; }
        public SortState Current { get; }

        public TrainStuffSortViewModel(SortState sortOrder)
        {
            DayOfWeekSort = sortOrder == SortState.DayOfWeekAsc ? SortState.DayOfWeekDesc : SortState.DayOfWeekAsc;
            Current = sortOrder;
        }
    }
}
