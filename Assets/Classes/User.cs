using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class User
{
    [DataMember]
    public long userId { get; set; }
    
    [DataMember]
    public string name { get; set; }
    
    [DataMember]
    public long activeSkinId { get; set; }
    
    [DataMember]
    public long credits { get; set; }
    
    [DataMember]
    public List<Skin> skins { get; set; }
    
    [DataMember]
    public List<Carriage> carriages { get; set; }
    
    [DataMember]
    public List<long> eggIds { get; set; }
    
    [DataMember]
    public string currentTrainRide { get; set; }
}