using GpsGeofenceApp.Models;    
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsGeofenceApp.Services
{
    public interface IGpsService {
        Task<GpsLocation?> GetCurrentLocationAsync();
        void StartTracking();
        void StopTracking();
        event Action<GpsLocation>? LocationChanged;
    }
}
