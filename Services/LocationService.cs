using System;

public class LocationService
{
    public async Task<Location?> GetCurrentLocationAsync()
    {
        var request = new GeolocationRequest(
            GeolocationAccuracy.Medium,
            TimeSpan.FromSeconds(10));

        return await Geolocation.GetLocationAsync(request);
    }
}
