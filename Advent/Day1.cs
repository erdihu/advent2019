using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public class Day1
    {
        private static IEnumerable<int> ReadInput()
        {
            var lines = File.ReadAllLines("Inputs/1.txt");
            foreach (var line in lines)
            {
                yield return int.Parse(line);
            }
        }

        private static int GetRequiredFuel(int mass)
        {
            return mass / 3 - 2;

        }

        private static int GetFuelDeep(int mass)
        {
            var fuelNeeded = GetRequiredFuel(mass);
            var additional = GetRequiredFuel(fuelNeeded);
            while (additional >= 0)
            {
                fuelNeeded += additional;
                additional = GetRequiredFuel(additional);
            }

            return fuelNeeded;
        }

        public int GetResultPart1()
        {
            var masses = ReadInput();

            return masses.Select(GetRequiredFuel).Sum();
        }

        public int GetResultPart2()
        {
            var masses = ReadInput();

            return masses.Select(GetFuelDeep).Sum();
        }
    }


}
