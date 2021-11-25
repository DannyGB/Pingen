using System.Collections.Generic;
using System.Linq;

namespace pin_number_gen.application
{
    public class PingenConfig
    {
        public IList<int> Range { get; set; }
        public IList<string> Exclusions { get; set; }

        public override string ToString()
        {
            return @$"Range: {Range.First()} - {Range.Skip(1).Take(1).First()}
Exclusions: {string.Join(',', Exclusions)}";
        }
    }
}