using System;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using pin_number_gen.infrastructure;

namespace pin_number_gen.application
{
    public class PinGeneratorService : IPinGeneratorService
    {
        private readonly IPinRepository _pinRepository;
        private readonly IMessageSession _session;
        private readonly PingenConfig _config;
        private readonly ILogger<IPinGeneratorService> _logger;
       
        public PinGeneratorService(IPinRepository pinRepository, IMessageSession session, IOptions<PingenConfig> options, ILogger<IPinGeneratorService> logger)
        {
            _config = options.Value;
            _pinRepository = pinRepository;
            _session = session;
            _logger = logger;
        }

        public async Task<GeneratePinResponse> GeneratePin(GeneratePinRequest request)
        {
            _logger.LogInformation("Generating new Pin number");

            await _pinRepository.Initialise(new Range(_config.Range.First(), _config.Range.Skip(1).Take(1).First()), _config.Exclusions.ToList());
            var pin = await _pinRepository.Next();

            await _session.SendLocal(new PinGeneratedMessage() { Pin = pin });

            return new GeneratePinResponse
            {
                Pin = pin
            };
        }
    }
}
