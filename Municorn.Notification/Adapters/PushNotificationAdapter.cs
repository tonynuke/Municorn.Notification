using System;
using System.Threading;
using System.Threading.Tasks;

namespace Municorn.Notification.Adapters
{
    /// <summary>
    /// Push notification adapter.
    /// </summary>
    public class PushNotificationAdapter : IPushNotificationAdapter
    {
        private const int NotDeliveredAttemptNumber = 5;
        private readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(() => new Random());
        private readonly object _locker = new object();
        private uint _sendingCount = 0;

        /// <inheritdoc/>
        public async Task<DeliveryStatus> Send(SendNotificationRequest notification)
        {
            var delayMilliseconds = _random.Value.Next(500, 2000);

            // simulate notification sending...
            await Task.Delay(delayMilliseconds);

            lock (_locker)
            {
                _sendingCount++;

                if (_sendingCount != NotDeliveredAttemptNumber)
                {
                    return DeliveryStatus.Delivered;
                }

                _sendingCount = 0;
                return DeliveryStatus.NotDelivered;
            }
        }
    }
}