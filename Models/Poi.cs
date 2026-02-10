using System;

namespace GpsGeofenceApp.Models
{
    public class Poi
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }

        public string AudioFile { get; set; } = "";

        public DateTime? LastExitTime { get; set; }

        // Added so GeofenceService.CanTrigger can compile.
        // Default 10 minutes; change as needed.
        public int CooldownMinutes { get; set; } = 10;
    }
}
