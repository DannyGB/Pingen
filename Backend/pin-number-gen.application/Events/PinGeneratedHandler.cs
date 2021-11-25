using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace pin_number_gen.application
{
    public class PinGeneratedHandler : IHandleMessages<PinGeneratedMessage>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<PinGeneratedHandler> _logger;

        public PinGeneratedHandler(IDistributedCache distributedCache, ILogger<PinGeneratedHandler> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }
        public async Task Handle(PinGeneratedMessage message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Processing MessageId {context.MessageId} & Pin {message.Pin}");

            await _distributedCache.SetStringAsync(Constants.LAST_GENERATED_PIN, message.Pin);
        }
    }
}