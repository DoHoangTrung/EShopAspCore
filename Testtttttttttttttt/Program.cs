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
            const int cannonNumber = 7;
            const int cardNumber = 4;

            const int idxStartCannon = 1;
            const int idxEndCannon = idxStartCannon + cannonNumber - 1;
            const int idxStartCard = idxEndCannon + 1;
            const int idxEndCard = idxStartCannon + cardNumber - 1; //(4): number of card
            Console.WriteLine(idxStartCannon);
            Console.WriteLine(idxEndCannon);
            Console.WriteLine(idxStartCard);
            Console.WriteLine(idxEndCard);

        }
    }
}
