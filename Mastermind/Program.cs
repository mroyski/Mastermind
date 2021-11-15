using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            RunGame(10);
        }

        public static void RunGame(int maxTurns)
        {
            var currentTurn = 0;
            var answer = GenerateAnswer();
            while (currentTurn < maxTurns)
            {
                Console.WriteLine($"Current Turn: {currentTurn + 1}");
                var input = GetUserInput();
                if (GuessIsCorrect(answer, input))
                {
                    Console.WriteLine("You Win!!!");
                    break;
                }
                else
                {
                    var matches = FindMatches(answer, input);
                    Console.WriteLine($"Result: {matches}");
                }

                currentTurn++;

                if (currentTurn == maxTurns)
                {
                    Console.WriteLine($"Correct Answer: {answer}");
                    Console.WriteLine("Game Over");
                }
            }
        }

        public static string GenerateAnswer()
        {
            Random random = new Random();
            var answer = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                answer.Append(random.Next(1, 6));
            }

            return answer.ToString();
        }

        public static string GetUserInput()
        {
            var validInput = false;
            var input = "";

            Console.WriteLine("Please enter your guess as a single string...");
            while (!validInput)
            {
                input = Console.ReadLine();
                if (input.Length == 4) break;

                Console.WriteLine("Please enter 4 digits");
            }

            return input;
        }

        public static bool GuessIsCorrect(string answer, string input)
        {
            return answer == input;
        }

        public static string FindMatches(string answer, string input)
        {
            var answerSB = new StringBuilder(answer);
            var inputSB = new StringBuilder(input);
            var matchResult = "";

            // Find occurances of exact matches
            // Mark exact match locations with x
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == answer[i])
                {
                    matchResult += "+";
                    answerSB[i] = 'x';
                    inputSB[i] = 'x';
                }
            }

            // Find occurances of matches that are at wrong position
            // Mark answer string with x to remove it from further checks
            for (int j = 0; j < inputSB.Length; j++)
            {
                var digit = inputSB[j];
                if (digit == 'x') continue;

                var matchingIndex = answerSB.ToString().IndexOf(digit);
                if (matchingIndex >= 0)
                {
                    matchResult += "-";
                    answerSB[matchingIndex] = 'x';
                }
            }
            
            return matchResult;
        }
    }
}