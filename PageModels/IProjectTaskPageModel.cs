using CommunityToolkit.Mvvm.Input;
using GpsGeofenceApp.Models;

namespace GpsGeofenceApp.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}