using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Municorn.Notification.Adapters;
using Xunit;

namespace Municorn.Notification.Tests
{
    public class PushNotificationAdapterTests
    {
        [Theory]
        [InlineData(4, 0)]
        [InlineData(5, 1)]
        [InlineData(100, 20)]
        public async Task Every_5_send_result_should_be_failed(
            int totalAttempts, int expectedFailedAttempts)
        {
            var adapter = new PushNotificationAdapter();

            var sendTasks = Enumerable.Range(0, totalAttempts).Select(attempt => adapter.Send(null));
            var sendResults = await Task.WhenAll(sendTasks);

            var actualFailedAttempts = sendResults.Count(result => result == Notification.DeliveryStatus.NotDelivered);
            actualFailedAttempts.Should().Be(expectedFailedAttempts);
        }
    }
}
