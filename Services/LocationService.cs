using System;
using System.Runtime.Versioning;

public class LocationService
{
    [SupportedOSPlatform("windows10.0.17763.0")]
    public async Task<Location?> GetCurrentLocationAsync()
    {
        var request = new GeolocationRequest(
            GeolocationAccuracy.Medium,
            TimeSpan.FromSeconds(10));

        return await Geolocation.GetLocationAsync(request);
    }
}
