using System.Collections.Generic;
using System.Threading.Tasks;
using GpsGeofenceApp.Models;

public interface IPoiRepository
{
    Task<List<Poi>> GetAllAsync();
    Task<Poi?> GetNearestAsync(double lat, double lng);
    Task UpdateAsync(Poi poi);
}
