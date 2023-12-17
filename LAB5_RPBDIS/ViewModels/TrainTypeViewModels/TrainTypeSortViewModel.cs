using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.TrainTypeViewModels
{
    public class TrainTypeSortViewModel
    {
        public SortState NameSort { get; }
        public SortState DistanceInKmSort { get; }
        public SortState TrainTypeSort { get; }
        public SortState Current { get; }

        public TrainTypeSortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            DistanceInKmSort = sortOrder == SortState.DistanceInKmAsc ? SortState.DistanceInKmDesc : SortState.DistanceInKmAsc;
            TrainTypeSort = sortOrder == SortState.TrainTypeAsc ? SortState.TrainTypeDesc : SortState.TrainTypeAsc;
            Current = sortOrder;
        }
    }
}
