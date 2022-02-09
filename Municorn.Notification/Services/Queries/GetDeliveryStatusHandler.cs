using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Municorn.Notification.Persistence;
using Municorn.Notification.Services.Commands;

namespace Municorn.Notification.Services.Queries
{
    /// <summary>
    /// <see cref="SendPushNotificationCommand"/> handler.
    /// </summary>
    public sealed class GetDeliveryStatusHandler
        : IRequestHandler<GetDeliveryStatusQuery, Maybe<DeliveryStatusResponse>>
    {
        private readonly DataContext _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDeliveryStatusHandler"/> class.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        public GetDeliveryStatusHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <inheritdoc/>
        public Task<Maybe<DeliveryStatusResponse>> Handle(GetDeliveryStatusQuery request, CancellationToken cancellationToken)
        {
            var notificationOrNull = _dataContext.Notifications
                .SingleOrDefault(notification => notification.Id == request.NotificationId);

            if (notificationOrNull == null)
            {
                return Task.FromResult(Maybe<DeliveryStatusResponse>.None);
            }

            Maybe<DeliveryStatusResponse> result = new DeliveryStatusResponse { Status = notificationOrNull.Status };
            return Task.FromResult(result);
        }
    }
}