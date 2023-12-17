using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.TrainViewModels
{
    public class TrainSortViewModel
    {
        public SortState NameSort { get; }
        public SortState DistanceInKmSort { get; }
        public SortState TrainTypeSort { get; }
        public SortState Current { get; }

        public TrainSortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            DistanceInKmSort = sortOrder == SortState.DistanceInKmAsc ? SortState.DistanceInKmDesc : SortState.DistanceInKmAsc;
            TrainTypeSort = sortOrder == SortState.TrainTypeAsc ? SortState.TrainTypeDesc : SortState.TrainTypeAsc;
            Current = sortOrder;
        }
    }
}
