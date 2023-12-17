using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.TrainTypeViewModels
{
    public class TrainTypeSortViewModel
    {
        public SortState NameSort { get; }
        public SortState Current { get; }

        public TrainTypeSortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            Current = sortOrder;
        }
    }
}
