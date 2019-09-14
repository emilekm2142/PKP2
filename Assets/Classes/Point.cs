using System.Runtime.Serialization;

[DataContract]
public class Point
{
    [DataMember]
    public long pointId { get; set; }
    
    [DataMember]
    public string stationName { get; set; }
    
    [DataMember]
    public double lng { get; set; }
    
    [DataMember]
    public double lat { get; set; }
}