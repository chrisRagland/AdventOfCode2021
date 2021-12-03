using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
	class Program
	{
		static void Main(string[] args)
		{
			//SolveDay1();
			//Console.WriteLine();
			//SolveDay2();
			//Console.WriteLine();
			SolveDay3();
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

		private static void SolveDay3()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\3.txt");

			int[] count;
			string gammaRate = string.Empty;

			bool doneOxygen = false;
			string oxygenValue = string.Empty;
			int[] oxygenCount;
			bool[] oxygen = Enumerable.Repeat(true, input.Length).ToArray();
			bool[] zeroOxygen;
			bool[] oneOxygen;

			bool doneCo2 = false;
			string co2Value = string.Empty;
			int[] co2Count;
			bool[] co2 = Enumerable.Repeat(true, input.Length).ToArray();
			bool[] zeroCo2;
			bool[] oneCo2;

			Stopwatch sw = new();

			//Part 1 & Part 2
			sw.Start();
			for (int i = 0; i < input[0].Length; i++)
			{
				count = new int[2];

				oxygenCount = new int[2];
				zeroOxygen = new bool[input.Length];
				oneOxygen = new bool[input.Length];

				co2Count = new int[2];
				zeroCo2 = new bool[input.Length];
				oneCo2 = new bool[input.Length];

				for (int j = 0; j < input.Length; j++)
				{
					var item = input[j];
					switch (item[i])
					{
						case '0':
							count[0]++;

							if (!doneOxygen && oxygen[j])
							{
								zeroOxygen[j] = true;
								oxygenCount[0]++;
							}

							if (!doneCo2 && co2[j])
							{
								zeroCo2[j] = true;
								co2Count[0]++;
							}

							break;
						case '1':
							count[1]++;

							if (!doneOxygen && oxygen[j])
							{
								oneOxygen[j] = true;
								oxygenCount[1]++;
							}

							if (!doneCo2 && co2[j])
							{
								oneCo2[j] = true;
								co2Count[1]++;
							}

							break;
						default:
							break;
					}
				}

				if (count[0] > count[1])
				{
					gammaRate += "0";
				}
				else
				{
					gammaRate += "1";
				}

				if (!doneOxygen)
				{
					if (oxygenCount[0] > oxygenCount[1])
					{
						oxygen = zeroOxygen;
						if (oxygenCount[0] == 1)
						{
							oxygenValue = input[(oxygen.TakeWhile(x => !x).Count())];
							doneOxygen = true;
						}
					}
					else
					{
						oxygen = oneOxygen;
						if (oxygenCount[1] == 1)
						{
							oxygenValue = input[(oxygen.TakeWhile(x => !x).Count())];
							doneOxygen = true;
						}
					}
				}

				if (!doneCo2)
				{
					if (co2Count[0] > co2Count[1])
					{
						co2 = oneCo2;
						if (co2Count[0] == 1)
						{
							co2Value = input[(co2.TakeWhile(x => !x).Count())];
							doneCo2 = true;
						}
					}
					else
					{
						co2 = zeroCo2;
						if (co2Count[1] == 1)
						{
							co2Value = input[(co2.TakeWhile(x => !x).Count())];
							doneCo2 = true;
						}
					}
				}
			}
			sw.Stop();

			var epsilonRate = new string(gammaRate.Select(x => x == '0' ? '1' : '0').ToArray());

			var partOneAnswer = Convert.ToInt32(gammaRate,2) * Convert.ToInt32(epsilonRate,2);
			var partTwoAnswer = Convert.ToInt32(oxygenValue, 2) * Convert.ToInt32(co2Value, 2);

			Console.WriteLine($"Day 3, Part 1: Power consumption: {partOneAnswer}");
			Console.WriteLine($"Day 3, Part 2: Life support rating: {partTwoAnswer}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}
	}
}