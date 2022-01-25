using System;
using System.Collections.Generic;
using System.Linq;

namespace Testtttttttttttttt
{
    class Program
    {
        public static List<List<T>> ChunkBy<T>(List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        static void Main(string[] args)
        {
            List<int> ids = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }; // 10 elements

            var result = ChunkBy<int>(ids, 4);
            foreach (var item in result)
            {

            }
        }
    }
}
