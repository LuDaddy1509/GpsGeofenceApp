using SQLite;

public class PoiEntity
{
    [PrimaryKey]
    public int Id { get; set; }

    public string Name { get; set; } = "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Radius { get; set; }
    public string AudioFile { get; set; } = "";

    public DateTime? LastExitTime { get; set; }
}
