using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using pin_number_gen.application;
using System.Text.Json;

namespace PinNumberGen.Function
{
    public class PinNumberGen
    {
        private readonly IPinGeneratorService _service;

        public PinNumberGen(IPinGeneratorService service)
        {
            _service = service;
        }

        [Function("Pingen")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Pingen");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _service.GeneratePin(new GeneratePinRequest());

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            response.WriteString(JsonSerializer.Serialize(result));
            
            return response;
        }
    }
}
