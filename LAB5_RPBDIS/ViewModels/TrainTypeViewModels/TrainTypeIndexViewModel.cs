using RailwayTrafficSolution.Models;
using RailwayTrafficSolution.ViewModels.EmployeeViewModels;

namespace RailwayTrafficSolution.ViewModels.TrainTypeViewModels
{
    public class TrainTypeIndexViewModel
    {
        public IEnumerable<TrainType> TrainTypes { get; }
        public PageViewModel PageViewModel { get; }
        public TrainTypeFilterViewModel FilterViewModel { get; }
        public TrainTypeSortViewModel SortViewModel { get; }
        public TrainTypeIndexViewModel(IEnumerable<TrainType> trainTypes, PageViewModel pageViewModel,
            TrainTypeFilterViewModel filterViewModel, TrainTypeSortViewModel sortViewModel)
        {
            TrainTypes = trainTypes;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}
