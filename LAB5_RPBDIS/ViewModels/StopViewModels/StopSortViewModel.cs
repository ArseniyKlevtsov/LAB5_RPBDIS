using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.StopViewModels
{
    public class StopSortViewModel
    {
        public SortState NameSort { get; }
        public SortState Current { get; }

        public StopSortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            Current = sortOrder;
        }
    }
}
