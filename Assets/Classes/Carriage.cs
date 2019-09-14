using System.Runtime.Serialization;

[DataContract]
public class Carriage
{
    [DataMember]
    public long carriageId { get; set; }
    
    [DataMember]
    public string carriageType { get; set; }

}