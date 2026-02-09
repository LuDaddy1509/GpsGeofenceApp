using GpsGeofenceApp.Models;
using System;

public interface IGeofenceService
{
    void SetPois(IEnumerable<Poi> pois);
    void Check(GpsLocation location);
    event Action<Poi>? Entered;
}
