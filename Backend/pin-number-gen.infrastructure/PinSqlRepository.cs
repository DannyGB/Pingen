using pin_number_gen.infrastructure.Extensions;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;

namespace pin_number_gen.infrastructure
{
    public class PinSqlRepository : IPinRepository
    {
        private List<string> _pinNumbers = new List<string>();
        private readonly PinDbContext _dbContext;
        public int Position { get; set; }

        public PinSqlRepository(PinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Initialise(Range range, List<string> exclusions = null)
        {
            if(!_dbContext.PinEntities.Any())
            {
                await GenerateValidPins(range, exclusions);
            }
        }

        public async Task<string> Next()
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var pin = _dbContext.PinEntities.FirstOrDefault(p => p.IsAvailable);

            if(pin != null)
            {
                pin.IsAvailable = false;
            }

            else
            {
                await _dbContext.PinEntities.Where(p => !p.IsAvailable).ForEachAsync(p => p.IsAvailable = true);
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return pin?.Pin;
        }

        private async Task GenerateValidPins(Range range, List<string> exclusions = null)
        {
            var pins = Enumerable.Range(range.Start.Value, range.End.Value)
                    .AsPin()
                    .Except(exclusions)
                    .Randomise()
                    .ToList();

            _dbContext.PinEntities.AddRange(pins.Select(p => new PinEntity() { Pin = p }));
            await _dbContext.SaveChangesAsync();
        }
    }
}
