using System;
using GpsGeofenceApp.Models;

public class GeofenceService: IGeofenceService
{
    private readonly Dictionary<string, Poi> _activePois = new();
    private readonly HashSet<string> _entered = new();
    public event Action<Poi>? Entered;
    public void SetPois(IEnumerable<Poi> pois)
    {
        _activePois.Clear();
        _entered.Clear();
        foreach (var poi in pois)
        {
            _activePois[poi.Id] = poi;
        }
        _entered.Clear();
    }
    public void Check(GpsLocation location)
    {
        foreach (var poi in _activePois.Values)
        {
            var distince = DistanceInMeter(location.Latitude,location.Longitude,poi.Latitude,poi.Longitude);
            if (distince <= poi.Radius)
            {
                if (_entered.Add(poi.Id))
                {
                    Entered?.Invoke(poi);
                }
            }

        }
    }
    private double DistanceInMeter(double lat1,double lat2,double lon1,double lon2)
    {
        var R = 6371000;
        var dLat = ToRadias(lat2 - lat1);
        var dLon = ToRadias(lon2 - lon1);
        var a = Math.Sin(dLat/2)* Math.Sin(dLat/2)+Math.Cos(ToRadias(lat2))* Math.Cos(ToRadias(lat2))*Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }
    private double ToRadias (double angle)
    {
        return angle * Math.PI / 180;
    }
}