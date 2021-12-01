using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
	class Program
	{
		static void Main(string[] args)
		{
			SolveDay1();
		}

		private static void SolveDay1()
		{
			//Initial Setup
			int[] input = File.ReadAllLines(@"input\1a.txt").Select(x => int.Parse(x)).ToArray();
			int count = 0;

			//Part 1
			int a = input[0];
			int b = 0;
			for (int i = 1; i < input.Length; i++)
			{
				b = input[i];
				if (b > a)
					count++;
				a = b;
			}

			Console.WriteLine($"Day 1, Part 1: {count} measurements larger.");

			//Intermission
			Console.WriteLine();
			count = 0;

			//Part 2
			int windowA = input[0] + input[1] + input[2];
			int windowB = 0;

			for (int i = 1; i < input.Length - 2; i++)
			{
				windowB = input[i] + input[(i + 1)] + input[(i + 2)];
				if (windowB > windowA)
					count++;
				windowA = windowB;
			}

			Console.WriteLine($"Day 1, Part 2: {count} sliding windows larger.");
		}
	}
}