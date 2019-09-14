using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class ApiTrain
{
    [DataMember]
    public long trainId { get; set; }
    
    [DataMember]
    public string trainType { get; set; }
}