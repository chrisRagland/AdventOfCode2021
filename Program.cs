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
			//SolveDay3();
			//Console.WriteLine();
			//SolveDay4();
			//Console.WriteLine();
			//SolveDay5();
			//Console.WriteLine();
			//SolveDay6();

			SolveDay7();

			/*

			//var input = File.ReadAllLines(@"input\test.txt");
			var input = File.ReadAllLines(@"input\7.txt");

			var split = input[0].Split(',').Select(x => int.Parse(x));
			var min = split.Min();
			var max = split.Max();
			
			Dictionary<int, long> scores = new Dictionary<int, long>();

			for (int i = min; i <= max; i++)
			{
				long thisScore = 0;
				foreach (var item in split)
				{
					thisScore += Enumerable.Range(1, Math.Abs(item - i)).Sum(x => x);
				}
				scores.Add(i, thisScore);
			}

			var low = scores.Min(x => x.Value);
			*/

			/*

			for (int i = min; i <= max; i++)
			{
				int thisScore = 0;
				foreach (var item in split)
				{
					thisScore += Math.Abs(item - i);
				}
				scores.Add(i, thisScore);
			}

			var low = scores.Min(x => x.Value);

			*/
		}

		private static void SolveDay1()
		{
			//Initial Setup
			int[] input = File.ReadAllLines(@"input\1.txt").Select(x => int.Parse(x)).ToArray();

			Stopwatch sw = new();
			sw.Start();
			int day1Part1Solution = 0;
			int day1Part2Solution = 0;

			//Part 1
			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] > input[(i - 1)])
					day1Part1Solution++;
			}

			//Part 2
			for (int i = 1; i < input.Length - 2; i++)
			{
				if (input[(i + 2)] > input[(i - 1)])
					day1Part2Solution++;
			}

			sw.Stop();
			Console.WriteLine($"Day 1, Part 1: {day1Part1Solution}");
			Console.WriteLine($"Day 1, Part 2: {day1Part2Solution}");
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
				}).ToArray();

			Stopwatch sw = new();
			sw.Start();

			int day2Part1Solution = 0;
			int day2Part2Solution = 0;

			var horizontal = 0;
			var depth = 0;
			var aim = 0;

			//Part 1 & Part 2
			for (int i = 0; i < input.Length; i++)
			{
				switch (input[i].Command)
				{
					case "forward":
						horizontal += input[i].XValue;
						aim += (depth * input[i].XValue);
						break;
					case "down":
						depth += input[i].XValue;
						break;
					case "up":
						depth -= input[i].XValue;
						break;
					default:
						break;
				}
			}

			day2Part1Solution = horizontal * depth;
			day2Part2Solution = horizontal * aim;

			sw.Stop();
			Console.WriteLine($"Day 2, Part 1: {day2Part1Solution}");
			Console.WriteLine($"Day 2, Part 2: {day2Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		private static void SolveDay3()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\3.txt");

			Stopwatch sw = new();
			sw.Start();

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

			//Part 1 & Part 2
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
							oxygenValue = input[Array.IndexOf(oxygen, true)];
							doneOxygen = true;
						}
					}
					else
					{
						oxygen = oneOxygen;
						if (oxygenCount[1] == 1)
						{
							oxygenValue = input[Array.IndexOf(oxygen, true)];
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
							co2Value = input[Array.IndexOf(co2,true)];
							doneCo2 = true;
						}
					}
					else
					{
						co2 = zeroCo2;
						if (co2Count[1] == 1)
						{
							co2Value = input[Array.IndexOf(co2, true)];
							doneCo2 = true;
						}
					}
				}
			}

			var epsilonRate = new string(gammaRate.Select(x => x == '0' ? '1' : '0').ToArray());

			var day3Part1Solution = Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
			var day3Part2Solution = Convert.ToInt32(oxygenValue, 2) * Convert.ToInt32(co2Value, 2);

			sw.Stop();
			Console.WriteLine($"Day 3, Part 1: {day3Part1Solution}");
			Console.WriteLine($"Day 3, Part 2: {day3Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public struct Day4BingoBoard
		{
			public int[] Layout { get; set; }
			public bool[] Called { get; set; }

			//Determine if the board has won
			public bool BoardWon
			{
				get
				{
					for (int i = 0; i < 5; i++)
					{
						if ((Called[i * 5] && Called[(i * 5) + 1] && Called[(i * 5) + 2] && Called[(i * 5) + 3] && Called[(i * 5) + 4])
							||
							(Called[i] && Called[i + 5] && Called[i + 10] && Called[i + 15] && Called[i + 20]))
						{
							return true;
						}
					}
					return false;
				}
			}

			//Help score won boards
			public int UncalledSum
			{
				get
				{
					int sum = 0;
					for (int i = 0; i < 25; i++)
					{
						if (!Called[i])
							sum += Layout[i];
					}
					return sum;
				}
			}
		}

		private static void SolveDay4()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\4.txt");

			var sw = new Stopwatch();
			sw.Start();

			var numberSequence = input[0].Split(',').Select(x => int.Parse(x)).ToList();
			List<Day4BingoBoard> Boards = new();
			int day4Part1Solution = 0;
			int day4Part2Solution = 0;

			//Board Setup
			for (int i = 2; i < input.Length; i += 6)
			{
				var newBoard = new Day4BingoBoard
				{
					Called = new bool[25],
					Layout = new int[25]
				};

				var a = input[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
				var b = input[i + 1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
				var c = input[i + 2].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
				var d = input[i + 3].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
				var e = input[i + 4].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
				for (int j = 0; j < 5; j++)
				{
					newBoard.Layout[j] = a[j];
					newBoard.Layout[j + 5] = b[j];
					newBoard.Layout[j + 10] = c[j];
					newBoard.Layout[j + 15] = d[j];
					newBoard.Layout[j + 20] = e[j];
				}

				Boards.Add(newBoard);
			}

			//Solved Board Setup
			int boardCount = Boards.Count;
			int boardsThatHaveWon = 0;
			bool[] DoneBoard = new bool[boardCount];

			//Play Bingo! Part 1 & Part 2
			foreach (var item in numberSequence)
			{
				for (int i = 0; i < Boards.Count; i++)
				{
					if (DoneBoard[i])
						continue;

					var board = Boards[i];
					for (int j = 0; j < 25; j++)
					{
						if (board.Layout[j] == item)
							board.Called[j] = true;
					}

					if (board.BoardWon)
					{
						boardsThatHaveWon++;
						DoneBoard[i] = true;

						if (boardsThatHaveWon == 1)
							day4Part1Solution = board.UncalledSum * item;

						if (boardsThatHaveWon == boardCount)
							day4Part2Solution = board.UncalledSum * item;
					}
				}

				if (boardsThatHaveWon == boardCount)
					break;
			}

			sw.Stop();
			Console.WriteLine($"Day 4, Part 1: {day4Part1Solution}");
			Console.WriteLine($"Day 4, Part 2: {day4Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public class Day5HydrothermalVents
		{
			private int[,] grid;
			public List<Day5VentLine> lines = new();

			protected void InitGrid()
			{
				int maxX = Math.Max(lines.Max(x => x.Point1.X), lines.Max(x => x.Point2.X));
				int maxY = Math.Max(lines.Max(x => x.Point1.Y), lines.Max(x => x.Point2.Y));
				int square = Math.Max(maxX, maxY);
				grid = new int[square + 1, square + 1];
			}

			public void ProcessStraightLines()
			{
				if (grid == null)
				{
					InitGrid();
				}

				foreach (var item in lines)
				{
					if (item.IsStraight)
					{
						if (item.Point1.X < item.Point2.X)
						{
							//Horizontal
							for (int i = item.Point1.X; i <= item.Point2.X; i++)
							{
								grid[i, item.Point1.Y]++;
							}
						}
						else
						{
							//Vertical
							for (int i = item.Point1.Y; i <= item.Point2.Y; i++)
							{
								grid[item.Point1.X, i]++;
							}
						}
					}
				}
			}

			public void ProcessDiagonalLines()
			{
				if (grid == null)
				{
					InitGrid();
				}

				foreach (var item in lines)
				{
					if (item.IsDiagonal)
					{
						int y = item.Point1.Y;
						for (int i = item.Point1.X; i <= item.Point2.X; i++)
						{
							grid[i, y]++;
							y += item.DiagonalYDiff;
						}
					}
				}
			}

			public void DrawCurrentGrid()
			{
				for (int i = 0; i < grid.GetLength(0); i++)
				{
					for (int j = 0; j < grid.GetLength(1); j++)
					{
						if (grid[j, i] > 0)
							Console.Write(grid[j, i]);
						else
							Console.Write('.');
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}

			public int CurrentOverlappingGridScore()
			{
				var score = 0;
				for (int i = 0; i < grid.GetLength(0); i++)
				{
					for (int j = 0; j < grid.GetLength(1); j++)
					{
						if (grid[i, j] > 1)
							score++;
					}
				}
				return score;
			}
		}

		public struct Day5VentLine
		{
			public Day5VentPoint Point1 { get; set; }
			public Day5VentPoint Point2 { get; set; }

			public bool IsStraight { get; set; }
			public bool IsDiagonal { get; set; }
			public int DiagonalYDiff { get; set; }

			public Day5VentLine(string input)
			{
				var cleanInput = input.Replace("->", "");
				var splitInput = cleanInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

				Day5VentPoint firstPoint = new(splitInput[0]);
				Day5VentPoint secondPoint = new(splitInput[1]);

				if (firstPoint.X == secondPoint.X || firstPoint.Y == secondPoint.Y)
				{
					IsStraight = true;
					IsDiagonal = false;
					DiagonalYDiff = 0;
					if (firstPoint.X > secondPoint.X || firstPoint.Y > secondPoint.Y)
					{
						Point1 = secondPoint;
						Point2 = firstPoint;
					}
					else
					{
						Point1 = firstPoint;
						Point2 = secondPoint;
					}
				}
				else
				{
					IsStraight = false;

					var xDiff = firstPoint.X - secondPoint.X;
					var yDiff = firstPoint.Y - secondPoint.Y;
					if (Math.Abs(xDiff) == Math.Abs(yDiff))
					{
						IsDiagonal = true;
						if (firstPoint.X > secondPoint.X)
						{
							Point1 = secondPoint;
							Point2 = firstPoint;
						}
						else
						{
							Point1 = firstPoint;
							Point2 = secondPoint;
						}

						if (Point1.Y > Point2.Y)
						{
							DiagonalYDiff = -1;
						}
						else
						{
							DiagonalYDiff = 1;
						}
					}
					else
					{
						IsDiagonal = false;
						DiagonalYDiff = 0;
						Point1 = firstPoint;
						Point2 = secondPoint;
					}
				}
			}
		}

		public struct Day5VentPoint
		{
			public int X { get; set; }
			public int Y { get; set; }

			public Day5VentPoint(string input)
			{
				var splitValues = input.Split(',').Select(x => int.Parse(x)).ToArray();
				X = splitValues[0];
				Y = splitValues[1];
			}
		}

		private static void SolveDay5()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\5.txt");

			var sw = new Stopwatch();
			sw.Start();

			var vents = new Day5HydrothermalVents();
			foreach (var item in input)
			{
				vents.lines.Add(new Day5VentLine(item));
			}

			//Part 1
			vents.ProcessStraightLines();
			var day5Part1Solution = vents.CurrentOverlappingGridScore();

			//Part 2
			vents.ProcessDiagonalLines();
			var day5Part2Solution = vents.CurrentOverlappingGridScore();

			sw.Stop();
			Console.WriteLine($"Day 5, Part 1: {day5Part1Solution}");
			Console.WriteLine($"Day 5, Part 2: {day5Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		private static void SolveDay6()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\6.txt");

			var sw = new Stopwatch();
			sw.Start();

			long[] fish = new long[9];
			var splitValues = input[0].Split(',').Select(x => int.Parse(x));
			foreach (var item in splitValues)
			{
				fish[item]++;
			}

			long day6Part1Solution = 0;
			long day6Part2Solution = 0;

			//Parts 1 & 2
			for (int i = 0; i < 256; i++)
			{
				long newSixFish = fish[0];

				for (int j = 1; j < fish.Length; j++)
					fish[(j - 1)] = fish[j];

				fish[6] += newSixFish;
				fish[8] = newSixFish;

				if (i == 79)
				{
					day6Part1Solution = fish.Sum(x => x);
				}

			}

			day6Part2Solution = fish.Sum(x => x);

			sw.Stop();
			Console.WriteLine($"Day 6, Part 1: {day6Part1Solution}");
			Console.WriteLine($"Day 6, Part 2: {day6Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		private static void SolveDay7()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\7.txt");

			Stopwatch sw = new();
			sw.Start();

			var split = input[0].Split(',').Select(x => int.Parse(x));
			var min = split.Min();
			var max = split.Max();

			long day7Part1Solution = long.MaxValue;
			long day7Part2Solution = long.MaxValue;

			//Part 1 & Part 2
			for (int i = min; i <= max; i++)
			{
				long part1Score = 0;
				long part2Score = 0;
				foreach (var item in split)
				{
					var itemScore = Math.Abs(item - i);
					part1Score += itemScore;
					part2Score += (itemScore * (itemScore + 1)) / 2;
				}
				if (part1Score < day7Part1Solution)
					day7Part1Solution = part1Score;
				if (part2Score < day7Part2Solution)
					day7Part2Solution = part2Score;
			}

			sw.Stop();
			Console.WriteLine($"Day 7, Part 1: {day7Part1Solution}");
			Console.WriteLine($"Day 7, Part 2: {day7Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}
	}
}