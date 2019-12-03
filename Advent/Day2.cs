using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public class Day2
    {
        private static IEnumerable<int> ReadInput()
        {
            var input = File.ReadLines("Inputs/2.txt");
            return input.First().Split(',').Select(int.Parse);
        }

        private int[] Process(int noun, int verb, int[] testArray = null)
        {
            var input = testArray ?? ReadInput().ToArray();

            input[1] = noun;
            input[2] = verb;

            for (var i = 0; i < input.Length; i += 4)
            {
                if (input[i] == 1)
                {
                    //add
                    var add = input[input[i + 1]] + input[input[i + 2]];
                    input[input[i + 3]] = add;
                }
                else if (input[i] == 2)
                {
                    //multiply
                    var multiply = input[input[i + 1]] * input[input[i + 2]];
                    input[input[i + 3]] = multiply;
                }
                else if (input[i] == 99)
                {
                    //stop
                    break;
                }
            }

            return input;
        }

        public int GetResultPart1()
        {
            var result = Process(12, 2);
            return result[0];
        }

        public string GetResultPart2()
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var result = Process(noun, verb);

                    if (result[0] == 19690720)
                    {
                        return $"noun={noun} verb={verb} 100 * noun + verb = {100 * noun + verb}";
                    }
                }
            }

            return "n/a";
        }
    }


}
