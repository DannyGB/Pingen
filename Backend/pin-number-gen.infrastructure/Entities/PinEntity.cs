
using System.ComponentModel.DataAnnotations.Schema;

namespace pin_number_gen.infrastructure
{
    public class PinEntity
    {
        public int Id { get; set; }
        public string Pin { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}