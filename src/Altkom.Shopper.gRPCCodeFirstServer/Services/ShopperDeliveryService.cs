using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.Shopper.Contracts;
using ProtoBuf.Grpc;

namespace Altkom.Shopper.gRPCCodeFirstServer.Services;

// dotnet add package protobuf-net.Grpc
public class ShopperDeliveryService : IDeliveryService
{
    private readonly ILogger<ShopperDeliveryService> logger;

    public ShopperDeliveryService(ILogger<ShopperDeliveryService> logger)
    {
        this.logger = logger;
    }

    public Task<ConfirmDeliveryResponse> ConfirmDeliveryAsync(ConfirmDeliveryRequest request, CallContext context = default)
    {
        logger.LogInformation("{ShippmentId} {ShippedDate} {Sign}", request.ShippmentId, request.ShippedDate, request.Sign);

        var response = new ConfirmDeliveryResponse { Cost = 10 };

        return Task.FromResult(response);
    }
}
