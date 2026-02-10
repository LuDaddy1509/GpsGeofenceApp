using System;
using System.Collections.Generic;
using GpsGeofenceApp.Models;

public class GeofenceService : IGeofenceService
{
    private readonly Dictionary<int, Poi> _pois = new();
    private readonly HashSet<int> _inside = new();
    private readonly Dictionary<int, DateTime> _lastTriggered = new();

    public event Action<Poi>? Entered;
    public event Action<Poi>? Exited;

    public void SetPois(IEnumerable<Poi> pois)
    {
        _pois.Clear();
        _inside.Clear();
        _lastTriggered.Clear();

        foreach (var poi in pois)
            _pois[poi.Id] = poi;
    }

    public void Check(GpsLocation location)
    {
        Poi? nearestPoi = null;
        double nearestDistance = double.MaxValue;

        foreach (var poi in _pois.Values)
        {
            var distance = DistanceInMeters(
                location.Latitude,
                location.Longitude,
                poi.Latitude,
                poi.Longitude);

            // tìm POI gần nhất
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPoi = poi;
            }

            bool isInside = distance <= poi.Radius;
            bool wasInside = _inside.Contains(poi.Id);

            // ENTER
            if (isInside && !wasInside && CanTrigger(poi))
            {
                _inside.Add(poi.Id);
                _lastTriggered[poi.Id] = DateTime.UtcNow;
                Entered?.Invoke(poi);
            }

            // EXIT
            if (!isInside && wasInside)
            {
                _inside.Remove(poi.Id);
                Exited?.Invoke(poi);
            }
        }
    }

    private bool CanTrigger(Poi poi)
    {
        if (!_lastTriggered.TryGetValue(poi.Id, out var last))
            return true;

        return DateTime.UtcNow - last >= TimeSpan.FromMinutes(poi.CooldownMinutes);
    }

    // ---- TOÁN HỌC ----
    private double DistanceInMeters(double lat1, double lon1, double lat2, double lon2)
    {
        var R = 6371000;
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private double ToRadians(double angle)
        => angle * Math.PI / 180;
}
