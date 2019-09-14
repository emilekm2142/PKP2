using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class TrainRide
{
    [DataMember]
    public string trainRideId { get; set; }
    
    [DataMember]
    public ApiTrain train { get; set;  }
    
    [DataMember]
    public List<Point> points { get; set; }
    
    [DataMember]
    public long lastVisitedPointId { get; set; }
}