using System.Runtime.Serialization;

[DataContract]
public class Point
{
    [DataMember]
    public long pointId { get; set; }
    
    [DataMember]
    public string city { get; set; }
    
    [DataMember]
    public double longitude { get; set; }
    
    [DataMember]
    public double lattitude { get; set; }
}