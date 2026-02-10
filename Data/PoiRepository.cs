using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GpsGeofenceApp.Models;

public class PoiRepository : IPoiRepository
{
    private readonly PoiLocalDataSource _local;

    public PoiRepository(PoiLocalDataSource local)
    {
        _local = local;
    }

    public async Task<List<Poi>> GetAllAsync()
    {
        return await _local.GetAllAsync();
    }

    public async Task<Poi?> GetNearestAsync(double lat, double lng)
    {
        var pois = await _local.GetAllAsync();

        return pois
            .OrderBy(p => GeoUtils.Distance(lat, lng, p.Latitude, p.Longitude))
            .FirstOrDefault();
    }

    public Task UpdateAsync(Poi poi)
    {
        return _local.UpdateAsync(poi);
    }
}
