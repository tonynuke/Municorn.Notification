using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Municorn.Notification.ComponentTests
{
    public class NotificationServiceTests
    {
        [Fact]
        public async Task Send_notification_and_get_delivery_status()
        {
            var application = new WebApplicationFactory<Program>();
            var httpClient = application.CreateClient();

            var grpcChannelOptions = new GrpcChannelOptions
            {
                HttpClient = httpClient,
            };
            var channel = GrpcChannel.ForAddress(httpClient.BaseAddress, grpcChannelOptions);
            var client = new Notifications.NotificationsClient(channel);

            var request = new SendNotificationRequest
            {
                Message = "message",
                Token = "token",
                AndroidConfig = new SendNotificationRequest.Types.AndroidConfig
                {
                    Title = "title"
                }
            };

            var requestsTasks = Enumerable.Range(0, 5).Select(async i => await client.SendNotificationAsync(request));
            var responses = await Task.WhenAll(requestsTasks);

            responses.Should().ContainSingle(response => response.Status == DeliveryStatus.NotDelivered);

            var getDeliveryStatusRequest = new GetDeliveryStatusRequest
            {
                Id = responses.Single(response => response.Status == DeliveryStatus.NotDelivered).Id
            };

            var notDeliveredState = await client.GetDeliveryStatusAsync(getDeliveryStatusRequest);
            notDeliveredState.Status.Should().Be(DeliveryStatus.NotDelivered);
        }
    }
}