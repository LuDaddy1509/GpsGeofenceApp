using GpsGeofenceApp.Models;

public class GpsService : IGpsService
{
    private CancellationTokenSource? _cts;
    private GpsLocation? _lastLocation;

    public event Action<GpsLocation>? LocationChanged;

    public async Task<GpsLocation?> GetCurrentLocationAsync()
    {
        try
        {
            var location = await GetLocationAsync(GeolocationAccuracy.Medium);
            return location == null ? null : Convert(location);
        }
        catch (FeatureNotEnabledException)
        {
            // Handle feature not enabled
            return null;
        }
        catch (PermissionException)
        {
            // Handle permission exception
            return null;
        }
    }

    public void StartTracking()
    {
        if (_cts != null) return;

        _cts = new CancellationTokenSource();
        _ = TrackingLoopAsync(_cts.Token);
    }

    public void StopTracking()
    {
        _cts?.Cancel();
        _cts = null;
    }

    private async Task TrackingLoopAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var accuracy = DecideAccuracy();
            Location? location = null;
            try
            {
                location = await GetLocationAsync(accuracy);
            }
            catch (FeatureNotEnabledException)
            {
                // Handle feature not enabled
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }

            if (location != null)
            {
                var gps = Convert(location);

                if (ShouldNotify(gps))
                {
                    _lastLocation = gps;
                    LocationChanged?.Invoke(gps);
                }
            }

            await Task.Delay(GetDelay(), token);
        }
    }

    private async Task<Location?> GetLocationAsync(GeolocationAccuracy accuracy)
    {
        var request = new GeolocationRequest(accuracy, TimeSpan.FromSeconds(10));
        return await Geolocation.GetLocationAsync(request);
    }

    private GeolocationAccuracy DecideAccuracy()
    {
        if (_lastLocation == null)
            return GeolocationAccuracy.Best;

        return GeolocationAccuracy.Medium;
    }

    private int GetDelay()
    {
        return _lastLocation == null ? 3000 : 8000;
    }

    private bool ShouldNotify(GpsLocation current)
    {
        if (_lastLocation == null) return true;

        var distance = DistanceInMeters(
            _lastLocation.Latitude,
            _lastLocation.Longitude,
            current.Latitude,
            current.Longitude);

        return distance > 5; // chỉ notify nếu di chuyển >5m
    }

    private GpsLocation Convert(Location location)
    {
        return new GpsLocation
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Accuracy = location.Accuracy ?? 0,
            Timestamp = location.Timestamp.UtcDateTime
        };
    }

    private double DistanceInMeters(double lat1, double lon1, double lat2, double lon2)
    {
        var R = 6371000; // bán kính Trái Đất theo mét
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private double ToRadians(double angle)
    {
        return angle * (Math.PI / 180);
    }
}