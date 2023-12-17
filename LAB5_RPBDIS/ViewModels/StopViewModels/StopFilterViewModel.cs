using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.StopViewModels
{
    public class StopFilterViewModel
    {
        public SelectList IsRailwayStationList { get; }
        public int SelectedIsRailwayStation { get; }

        public SelectList HasWaitingRoomList { get; }
        public int SelectedHasWaitingRoom { get; }
        public string SelectedName { get; }

        public StopFilterViewModel(int isRailwayStation, int hasWaitingRoom, string name)
        {
            SelectedListManager selectedListManager = new SelectedListManager();
            IsRailwayStationList = selectedListManager.GetBoolSelectedList(isRailwayStation);
            SelectedIsRailwayStation = isRailwayStation;
            HasWaitingRoomList = selectedListManager.GetBoolSelectedList(hasWaitingRoom);
            SelectedHasWaitingRoom = hasWaitingRoom;
            SelectedName = name;
        }
    }
}
