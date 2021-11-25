using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pin_number_gen.infrastructure
{
    public interface IPinRepository
    {
        Task Initialise(Range range, List<string> exclusions = null);
        Task<string> Next();
    }
}
