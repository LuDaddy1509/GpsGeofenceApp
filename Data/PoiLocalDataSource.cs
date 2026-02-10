using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using GpsGeofenceApp.Models;

public class PoiLocalDataSource
{
    private readonly SQLiteAsyncConnection _db;

    public PoiLocalDataSource(AppDatabase db)
    {
        _db = db.Connection ?? throw new ArgumentNullException(nameof(db.Connection));
    }

    public Task<List<Poi>> GetAllAsync()
        => _db.Table<Poi>().ToListAsync();

    public Task UpdateAsync(Poi poi)
        => _db.UpdateAsync(poi);
}
