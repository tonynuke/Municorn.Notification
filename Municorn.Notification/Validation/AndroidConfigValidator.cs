using FluentValidation;

namespace Municorn.Notification.Validation
{
    /// <summary>
    /// <see cref="SendNotificationRequest.Types.AndroidConfig"/> validator.
    /// </summary>
    public sealed class AndroidConfigValidator : AbstractValidator<SendNotificationRequest.Types.AndroidConfig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndroidConfigValidator"/> class.
        /// </summary>
        public AndroidConfigValidator()
        {
            RuleFor(notification => notification.Title).NotEmpty().MaximumLength(255);
            RuleFor(notification => notification.Condition).MaximumLength(2000);
        }
    }
}