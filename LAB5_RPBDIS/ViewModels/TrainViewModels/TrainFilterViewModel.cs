using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayTrafficSolution.Models;

namespace RailwayTrafficSolution.ViewModels.TrainViewModels
{
    public class TrainFilterViewModel
    {
        public SelectList TrainTypes { get; }
        public int SelectedTrainType { get; }
        public string SelectedName { get; }

        public TrainFilterViewModel(List<TrainType> trainTypes, int traintype, string name)
        {
            trainTypes.Insert(0, new TrainType { TypeName = "Все", TrainTypeId = 0 });

            TrainTypes = new SelectList(trainTypes, "TrainTypeId", "TypeName", name);
            SelectedName = name;
        }
    }
}
