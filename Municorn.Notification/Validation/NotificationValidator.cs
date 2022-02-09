namespace Municorn.Notification.Validation
{
    using FluentValidation;

    /// <summary>
    /// <see cref="Notification"/> validator.
    /// </summary>
    public sealed class NotificationValidator : AbstractValidator<SendNotificationRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationValidator"/> class.
        /// </summary>
        public NotificationValidator()
        {
            RuleFor(notification => notification.Token).NotEmpty().MaximumLength(50);
            RuleFor(notification => notification.Message).NotEmpty().MaximumLength(2000);

            When(notification => notification.AndroidConfig != null,
                () => RuleFor(notification => notification.AndroidConfig)
                    .NotNull()
                    .SetValidator(new AndroidConfigValidator()));
        }
    }
}