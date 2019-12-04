using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public class Day4
    {
        private static IEnumerable<int> GetRange()
        {
            var input = File.ReadAllLines("Inputs/4.txt")[0].Split('-');
            var min = int.Parse(input[0]);
            var max = int.Parse(input[1]);
            return Enumerable.Range(min, max - min + 1);
        }

        public string GetResultPart1()
        {
            var range = GetRange();

            var count = 0;
            foreach (var potentialPassword in range)
            {
                var digits = GetDigits(potentialPassword);
                if (TwoAdjacentDigitsAreSame(digits) &&
                    DigitsAreIncremental(digits))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        public string GetResultPart2()
        {
            var range = GetRange();

            var count = 0;
            foreach (var potentialPassword in range)
            {
                var digits = GetDigits(potentialPassword);
                if (MaxTwoAdjacentDigitsAreSameWithinAGroup(digits) &&
                    DigitsAreIncremental(digits))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        private static List<int> GetDigits(int n)
        {
            var stack = new Stack<int>(6);

            while (n > 0)
            {
                var digit = n % 10;
                stack.Push(digit);

                n /= 10;
            }

            return stack.ToList();
        }


        private static bool TwoAdjacentDigitsAreSame(List<int> digits)
        {
            for (var i = 0; i < digits.Count - 1; i++)
            {
                if (digits[i] == digits[i + 1]) return true;
            }

            return false;
        }
        private static bool MaxTwoAdjacentDigitsAreSameWithinAGroup(List<int> digits) //not 424
        {
            var dict = new Dictionary<int, int>(); //digit,count

            for (var i = 0; i < digits.Count - 1; i++)
            {
                if (digits[i] == digits[i + 1])
                {
                    if (!dict.ContainsKey(digits[i]))
                    {
                        dict.Add(digits[i], 2);
                    }
                    else
                    {
                        dict[digits[i]]++;
                    }
                }
            }

            return dict.Any(kvp => kvp.Value == 2);
        }

        private static bool DigitsAreIncremental(List<int> digits)
        {
            for (var i = 1; i < digits.Count; i++)
            {
                if (digits[i - 1] > digits[i]) return false;
            }

            return true;
        }
    }
}
