using System;

namespace Municorn.Notification.Persistence
{
    /// <summary>
    /// Notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="status">Status.</param>
        public Notification(DeliveryStatus status)
        {
            Id = Guid.NewGuid();
            Status = status;
        }

        /// <summary>
        /// Gets id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets delivery status.
        /// </summary>
        public DeliveryStatus Status { get; }
    }
}