using System.Threading.Tasks;

namespace Municorn.Notification.Adapters
{
    /// <summary>
    /// Push notification adapter interface.
    /// </summary>
    public interface IPushNotificationAdapter
    {
        /// <summary>
        /// Sends notification to external service via network connection.
        /// </summary>
        /// <param name="notification">Notification.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<DeliveryStatus> Send(SendNotificationRequest notification);
    }
}