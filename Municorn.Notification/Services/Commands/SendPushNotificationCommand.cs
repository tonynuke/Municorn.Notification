using CSharpFunctionalExtensions;
using MediatR;

namespace Municorn.Notification.Services.Commands
{
    /// <summary>
    /// Send push notification command.
    /// </summary>
    public sealed class SendPushNotificationCommand : IRequest<Result<SendNotificationResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendPushNotificationCommand"/> class.
        /// </summary>
        /// <param name="notification">Notification.</param>
        public SendPushNotificationCommand(SendNotificationRequest notification)
        {
            Notification = notification;
        }

        /// <summary>
        /// Gets notification.
        /// </summary>
        public SendNotificationRequest Notification { get; init; }
    }
}