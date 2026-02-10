using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using GpsGeofenceApp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GpsGeofenceApp.Data
{
    [JsonSerializable(typeof(Project))]
    [JsonSerializable(typeof(ProjectTask))]
    [JsonSerializable(typeof(ProjectsJson))]
    [JsonSerializable(typeof(Category))]
    [JsonSerializable(typeof(Tag))]
    [JsonSerializable(typeof(Poi))]
    public partial class JsonContext : JsonSerializerContext
    {
        public JsonContext(JsonSerializerOptions? options = null) : base(options) { }

        public async Task<List<Poi>> LoadPoisAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("pois.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<List<Poi>>(json)!;
        }
    }
}
