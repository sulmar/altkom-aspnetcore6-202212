using System.ServiceModel;
using System.Runtime.Serialization;
using ProtoBuf.Grpc;

namespace Altkom.Shopper.Contracts;


// dotnet add package System.ServiceModel.Primitives
[ServiceContract]
public interface IDeliveryService
{
    [OperationContract]
    Task<ConfirmDeliveryResponse> ConfirmDeliveryAsync(ConfirmDeliveryRequest request, CallContext context = default); // dotnet add package protobuf-net.Grpc
    
}

[DataContract]
public class ConfirmDeliveryRequest
{
    [DataMember(Order = 1)]
    public int ShippmentId { get; set; }
    [DataMember(Order = 2)]
    public DateTime ShippedDate { get; set; }
    [DataMember(Order = 3)]
    public string Sign { get; set; }

}

[DataContract]
public class ConfirmDeliveryResponse
{
    [DataMember(Order = 1)]
    public decimal Cost { get; set; }
}
