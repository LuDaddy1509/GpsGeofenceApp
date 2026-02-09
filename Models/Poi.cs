using System;
namespace GpsGeofenceApp.Models
{
public class Poi
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string Name { get; set; } = string.Empty;
	public double Latitude { get; set; }
	public double Longitude { get; set; }
	public double Radius { get; set; } = 30;// 30 meters
	public string Despription { get; set; } = string.Empty;
	public string? AudioUrl { get; set; }
}
}
