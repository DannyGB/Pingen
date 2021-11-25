using System.Threading.Tasks;

namespace pin_number_gen.application
{
    public interface IPinGeneratorService
    {
        Task<GeneratePinResponse> GeneratePin(GeneratePinRequest request);
    }
}