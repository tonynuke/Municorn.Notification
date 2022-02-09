using System;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Municorn.Notification.Services.Commands;
using Municorn.Notification.Services.Queries;

namespace Municorn.Notification.Services
{
    /// <summary>
    /// Notification service.
    /// </summary>
    public class NotificationService : Notifications.NotificationsBase
    {
        private static readonly Status NotificationNotFound = new Status(StatusCode.NotFound, "Notification not found");
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public NotificationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <inheritdoc/>
        public override async Task<SendNotificationResponse> SendNotification(SendNotificationRequest request, ServerCallContext context)
        {
            var command = new SendPushNotificationCommand(request);
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return result.Value;
            }

            var status = new Status(StatusCode.InvalidArgument, result.Error);
            throw new RpcException(status);
        }

        /// <inheritdoc/>
        public override async Task<DeliveryStatusResponse> GetDeliveryStatus(
            GetDeliveryStatusRequest request, ServerCallContext context)
        {
            var query = new GetDeliveryStatusQuery(Guid.Parse(request.Id));
            var statusOrNoting = await _mediator.Send(query);
            if (statusOrNoting.HasValue)
            {
                return statusOrNoting.Value;
            }

            throw new RpcException(NotificationNotFound);
        }
    }
}
