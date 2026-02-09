using System;
using System.Collections.Generic;
using System.Text;

namespace GpsGeofenceApp.Models
{
    public class GpsLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
