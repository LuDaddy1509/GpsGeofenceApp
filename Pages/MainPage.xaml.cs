using GpsGeofenceApp.Models;
using GpsGeofenceApp.PageModels;

namespace GpsGeofenceApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}