using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Municorn.Notification.Adapters;
using Municorn.Notification.Persistence;

namespace Municorn.Notification.Services.Commands
{
    /// <summary>
    /// <see cref="SendPushNotificationCommand"/> handler.
    /// </summary>
    public sealed class SendPushNotificationHandler
        : IRequestHandler<SendPushNotificationCommand, Result<SendNotificationResponse>>
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<SendNotificationRequest> _validator;
        private readonly IPushNotificationAdapter _notificationAdapter;
        private readonly ILogger<SendPushNotificationHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendPushNotificationHandler"/> class.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="validator">Validator.</param>
        /// <param name="notificationAdapter">Notification adapter.</param>
        /// <param name="logger">Logger.</param>
        public SendPushNotificationHandler(
            DataContext dataContext,
            IValidator<SendNotificationRequest> validator,
            IPushNotificationAdapter notificationAdapter,
            ILogger<SendPushNotificationHandler> logger)
        {
            _dataContext = dataContext;
            _validator = validator;
            _notificationAdapter = notificationAdapter;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<Result<SendNotificationResponse>> Handle(
            SendPushNotificationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request.Notification);
            if (!validationResult.IsValid)
            {
                return Result.Failure<SendNotificationResponse>(validationResult.ToString());
            }

            var sendResult = await _notificationAdapter.Send(request.Notification);
            var notification = new Persistence.Notification(sendResult);
            _dataContext.Notifications.Add(notification);

            _logger.LogTrace("Notification {notification}", request);

            return new SendNotificationResponse
            {
                Id = notification.Id.ToString(),
                Status = notification.Status
            };
        }
    }
}