using System.Runtime.Serialization;

[DataContract]
public class Skin
{
    [DataMember]
    public long skinId { get; set; }
    
    [DataMember]
    public long carriageId { get; set; }

}