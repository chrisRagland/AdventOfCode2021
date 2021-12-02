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
			Console.WriteLine();
			SolveDay2();
		}

		private static void SolveDay1()
		{
			//Initial Setup
			int[] input = File.ReadAllLines(@"input\1.txt").Select(x => int.Parse(x)).ToArray();
			Stopwatch sw = new();
			int count = 0;

			//Part 1
			sw.Start();
			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] > input[(i - 1)])
					count++;
			}
			sw.Stop();

			Console.WriteLine($"Day 1, Part 1: {count} measurements larger.");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");

			//Intermission
			Console.WriteLine();
			count = 0;
			sw.Reset();

			//Part 2
			sw.Start();
			for (int i = 1; i < input.Length - 2; i++)
			{
				if (input[(i + 2)] > input[(i - 1)])
					count++;
			}
			sw.Stop();

			Console.WriteLine($"Day 1, Part 2: {count} sliding windows larger.");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public struct Day2SubCommand
		{
			public string Command { get; set; }
			public int XValue { get; set; }
		}

		private static void SolveDay2()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\2.txt")
				.AsEnumerable()
				.Select(x => x.Split(' '))
				.Select(x => new Day2SubCommand() {
					Command = x[0],
					XValue = int.Parse(x[1])
				});
			Stopwatch sw = new();
			var horizontal = 0;
			var depth = 0;

			//Part 1
			sw.Start();
			foreach (var item in input)
			{
				switch (item.Command)
				{
					case "forward":
						horizontal += item.XValue;
						break;
					case "down":
						depth += item.XValue;
						break;
					case "up":
						depth -= item.XValue;
						break;
					default:
						break;
				}
			}
			sw.Stop();

			var part1Result = horizontal * depth;

			Console.WriteLine($"Day 2, Part 1: {part1Result}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");

			//Intermission
			Console.WriteLine();
			horizontal = 0;
			depth = 0;
			var aim = 0;
			sw.Reset();

			//Part 2
			sw.Start();
			foreach (var item in input)
			{
				switch (item.Command)
				{
					case "forward":
						horizontal += item.XValue;
						depth += (aim * item.XValue);
						break;
					case "down":
						aim += item.XValue;
						break;
					case "up":
						aim -= item.XValue;
						break;
					default:
						break;
				}
			}
			sw.Stop();

			var part2Result = horizontal * depth;

			Console.WriteLine($"Day 2, Part 2: {part2Result}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}
	}
}