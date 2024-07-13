using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Beltmatic
    {
        // Define the operators
        private static readonly char[] operators = { '+', '-', '*', '/', '^' };

        public static void Main()
        {
            while (1 == 1)
            {
                Console.WriteLine("Enter the target number:");
                int target = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the maximum number:");
                int maxNumber = int.Parse(Console.ReadLine());

                // Call the function to find the formula
                string formula = FindFormula(target, maxNumber);

                if (formula != null)
                {
                    Console.WriteLine($"Formula to reach {target}: {formula}");
                }
                else
                {
                    Console.WriteLine($"No formula found to reach {target} using numbers up to {maxNumber}");
                }
            }
        }

        private static string FindFormula(int target, int maxNumber)
        {
            var queue = new Queue<(int, string)>();
            var seen = new HashSet<int>();

            // Initialize queue with numbers 1 to maxNumber, excluding 10 because it never appears in the game
            for (int i = 1; i <= maxNumber; i++)
            {
                if (i != 10)
                {
                    string start = i.ToString();
                    queue.Enqueue((i, start));
                }
            }

            while (queue.Count > 0)
            {
                var (currentValue, expression) = queue.Dequeue();

                // If we have reached the target number, return the expression
                if (currentValue == target)
                {
                    return expression;
                }

                // Iterate through each operator and number to create new expressions
                foreach (char op in operators)
                {
                    for (int i = 1; i <= maxNumber; i++)
                    {
                        if (i % 10 != 0)
                        {
                            int newValue = Calculate(currentValue, i, op);
                            if (!seen.Contains(newValue))
                            {
                                if (newValue > maxNumber && Math.Abs(newValue) <= target * 2) // Ensure valid and reasonable calculation
                                {
                                    string newExpression = $"({expression} {op} {i})";
                                    queue.Enqueue((newValue, newExpression));
                                    seen.Add(newValue);
                                }
                            }
                        }
                    }
                }
            }

            return null; // If no formula found
        }

        private static int Calculate(int a, int b, char op)
        {
            return op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => b != 0 ? a / b : int.MinValue, // Prevent division by zero
                '^' => (int)Math.Pow(a, b),
                _ => int.MinValue,
            };
        }
    }
}
