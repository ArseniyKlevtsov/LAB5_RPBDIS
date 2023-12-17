using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayTrafficSolution.Models;

namespace RailwayTrafficSolution.ViewModels.TrainTypeViewModels
{
    public class TrainTypeFilterViewModel
    {
        public SelectList TrainTypes { get; }
        public int SelectedTrainType { get; }
        public string SelectedName { get; }

        public TrainTypeFilterViewModel(List<TrainType> trainTypes, int traintype, string name)
        {
            trainTypes.Insert(0, new TrainType { TypeName = "Все", TrainTypeId = 0 });
            
            TrainTypes = new SelectList(trainTypes, "TrainTypeId", "TypeName", name);
            SelectedName = name;
        }
    }
}
