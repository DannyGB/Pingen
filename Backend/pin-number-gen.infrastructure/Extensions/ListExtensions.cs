using System;
using System.Linq;
using System.Collections.Generic;

namespace pin_number_gen.infrastructure.Extensions
{
    public static class ListExtensions
    {
        private static Random _random = new Random();  

        public static IEnumerable<string> Randomise(this IEnumerable<string> list) => list.OrderBy(a => _random.Next()).ToList();
        
        public static IEnumerable<string> AsPin(this IEnumerable<int> list) => list.Select(x => x.ToString("0000"));
    }
}