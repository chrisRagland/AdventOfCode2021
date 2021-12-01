using System;
using System.Diagnostics;
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
			int[] input = File.ReadAllLines(@"input\1.txt").Select(x => int.Parse(x)).ToArray();
			Stopwatch sw = new();
			int count = 0;
			int a = input[0];
			int b = 0;

			//Part 1
			sw.Start();
			for (int i = 1; i < input.Length; i++)
			{
				b = input[i];
				if (b > a)
					count++;
				a = b;
			}
			sw.Stop();

			Console.WriteLine($"Day 1, Part 1: {count} measurements larger.");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");

			//Intermission
			Console.WriteLine();
			count = 0;
			a = input[0];
			sw.Reset();

			//Part 2
			sw.Start();
			for (int i = 1; i < input.Length - 2; i++)
			{
				b = input[(i + 2)];
				if (b > a)
					count++;
				a = input[i];
			}
			sw.Stop();

			Console.WriteLine($"Day 1, Part 2: {count} sliding windows larger.");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}
	}
}