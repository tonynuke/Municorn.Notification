using System.Collections.Generic;

namespace Municorn.Notification.Persistence
{
    /// <summary>
    /// Data context.
    /// </summary>
    public class DataContext
    {
        /// <summary>
        /// Gets notifications.
        /// </summary>
        public List<Notification> Notifications { get; } = new ();
    }
}