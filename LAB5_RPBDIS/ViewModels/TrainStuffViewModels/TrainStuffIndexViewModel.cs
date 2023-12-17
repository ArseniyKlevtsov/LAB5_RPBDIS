using RailwayTrafficSolution.Models;
using RailwayTrafficSolution.ViewModels.TrainStuffViewModels;

namespace RailwayTrafficSolution.ViewModels.TrainStuffViewModels
{
    public class TrainStuffIndexViewModel
    {
        public IEnumerable<TrainStaff> TrainStaffs { get; }
        public PageViewModel PageViewModel { get; }
        public TrainStuffFilterViewModel FilterViewModel { get; }
        public TrainStuffSortViewModel SortViewModel { get; }
        public TrainStuffIndexViewModel(IEnumerable<TrainStaff> trainStuffs, PageViewModel pageViewModel,
            TrainStuffFilterViewModel filterViewModel, TrainStuffSortViewModel sortViewModel)
        {
            TrainStaffs = trainStuffs;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}
