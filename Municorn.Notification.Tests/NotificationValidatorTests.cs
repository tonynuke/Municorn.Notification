using FluentAssertions;
using Municorn.Notification.Validation;
using Xunit;

namespace Municorn.Notification.Tests
{
    public class NotificationValidatorTests
    {
        private readonly NotificationValidator _validator = new();

        [Theory]
        [MemberData(nameof(Cases))]
        public void Validation_result_equals_to_template(SendNotificationRequest notification, bool expectedValidationResult)
        {
            var validationResult = _validator.Validate(notification);
            validationResult.IsValid.Should().Be(expectedValidationResult);
        }

        public static readonly TheoryData<SendNotificationRequest, bool> Cases = new TheoryData<SendNotificationRequest, bool>
        {
            {
                new SendNotificationRequest
                {
                    Message = "message",
                    Token = "123456qwerty",
                    AndroidConfig = new SendNotificationRequest.Types.AndroidConfig
                    {
                        Title = "title"
                    }
                },
                true
            },
            {
                new SendNotificationRequest
                {
                    Message = "message",
                    Token = "123456qwerty",
                    AndroidConfig = new SendNotificationRequest.Types.AndroidConfig()
                },
                false
            },
            {
                new SendNotificationRequest
                {
                    Message = "message",
                    Token = "123456qwerty",
                },
                true
            },
            {
                new SendNotificationRequest
                {
                    Message = "message",
                },
                false
            }
        };
    }
}