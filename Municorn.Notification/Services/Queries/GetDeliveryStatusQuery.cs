using System;
using CSharpFunctionalExtensions;
using MediatR;

namespace Municorn.Notification.Services.Queries
{
    /// <summary>
    /// Get notification delivery status query.
    /// </summary>
    public class GetDeliveryStatusQuery : IRequest<Maybe<DeliveryStatusResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDeliveryStatusQuery"/> class.
        /// </summary>
        /// <param name="notificationId">Notification id.</param>
        public GetDeliveryStatusQuery(Guid notificationId)
        {
            NotificationId = notificationId;
        }

        /// <summary>
        /// Gets notification id.
        /// </summary>
        public Guid NotificationId { get; init; }
    }
}