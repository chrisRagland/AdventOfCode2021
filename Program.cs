using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

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
			//Console.WriteLine();
			//SolveDay7();
			//Console.WriteLine();
			//SolveDay8();
			//Console.WriteLine();
			//SolveDay9();
			//Console.WriteLine();
			//SolveDay10();
			//Console.WriteLine();
			//SolveDay11();
			//Console.WriteLine();
			//SolveDay12();
			//Console.WriteLine();
			//SolveDay13();
			//Console.WriteLine();
			//SolveDay14();
			//Console.WriteLine();
			//SolveDay15();
			//Console.WriteLine();
			//Day16 day16 = new();
			//day16.Solve();
			//Console.WriteLine();
			//SolveDay17();
			//Console.WriteLine();
			//SolveDay18();
			//Console.WriteLine();
			//SolveDay19();
			//Console.WriteLine();
			//SolveDay20();
			//Console.WriteLine();
			Day21 day21 = new();
			day21.Solve();
		}

		public static void SolveDay1()
		{
			//Initial Setup
			int[] input = File.ReadAllLines(@"input\01.txt").Select(x => int.Parse(x)).ToArray();

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

		public static void SolveDay2()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\02.txt")
				.AsEnumerable()
				.Select(x => x.Split(' '))
				.Select(x => new Day2SubCommand()
				{
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

		public static void SolveDay3()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\03.txt");

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
							co2Value = input[Array.IndexOf(co2, true)];
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

		public static void SolveDay4()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\04.txt");

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

		public static void SolveDay5()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\05.txt");

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

		public static void SolveDay6()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\06.txt");

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

		public static void SolveDay7()
		{
			//Initial Setup
			var input = File.ReadAllLines(@"input\07.txt");

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

		public static void SolveDay8()
		{
			var input = File.ReadAllLines(@"input\08.txt");

			Stopwatch sw = new();
			sw.Start();

			int day8Part1Solution = 0;
			int day8Part2Solution = 0;

			foreach (var item in input)
			{
				var splitInput = item.Split('|');
				var tenDigits = splitInput[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => String.Concat(x.OrderBy(x => x))).OrderBy(x => x.Length).ToArray();
				var outputValue = splitInput[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => String.Concat(x.OrderBy(x => x))).ToArray();

				Dictionary<string, int> lookup = new();
				lookup.Add(tenDigits[0], 1);    //Digit 1 is our two char Digit
				lookup.Add(tenDigits[1], 7);    //Digit 7 is our three char Digit
				lookup.Add(tenDigits[2], 4);    //Digit 4 is our four char Digit
				lookup.Add(tenDigits[9], 8);    //Digit 8 is our nine char Digit

				for (int i = 3; i < 6; i++)
				{
					if (tenDigits[2].Intersect(tenDigits[i]).Count() == 2)
					{
						lookup.Add(tenDigits[i], 2); //Digit 2 is the only 5 char Digit to contain two parts of Digit 4
					}
					else if (tenDigits[0].Intersect(tenDigits[i]).Count() == 2)
					{
						lookup.Add(tenDigits[i], 3); //Digit 3 is the only 5 char Digit that contains more than two parts of Digit 4 and both parts of Digit 1
					}
					else
					{
						lookup.Add(tenDigits[i], 5); //Digit 5 is the only 5 char Digit that doesn't fit either of the above rules
					}
				}

				for (int i = 6; i < 9; i++)
				{
					if (tenDigits[0].Intersect(tenDigits[i]).Count() != 2)
					{
						lookup.Add(tenDigits[i], 6); //Digit 6 is the only 6 char Digit that doesn't contain all of Digit 1
					}
					else if (tenDigits[2].Intersect(tenDigits[i]).Count() != 4)
					{
						lookup.Add(tenDigits[i], 0); //Digit 0 is the only 6 char Digit that contains all of Digit 1 and doesn't contain all of Digit 4
					}
					else
					{
						lookup.Add(tenDigits[i], 9); //Digit 9 is the only 6 char Digit that doesn't fit either of the above rules
					}
				}

				var powValue = 3;
				var currentOutput = 0;
				for (int i = 0; i < outputValue.Length; i++)
				{
					//Part 1
					if (outputValue[i].Length == 2 || outputValue[i].Length == 4 || outputValue[i].Length == 3 || outputValue[i].Length == 7)
						day8Part1Solution++;

					//Part 2
					currentOutput += ((int)Math.Pow(10, powValue)) * lookup[outputValue[i]];
					powValue--;
				}

				day8Part2Solution += currentOutput;

			}

			sw.Stop();
			Console.WriteLine($"Day 8, Part 1: {day8Part1Solution}");
			Console.WriteLine($"Day 8, Part 2: {day8Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public struct Day9GridPoint
		{
			public int I { get; set; }
			public int J { get; set; }
		}

		public static void SolveDay9()
		{
			var input = File.ReadAllLines(@"input\09.txt");

			Stopwatch sw = new();
			sw.Start();

			int day9Part1Solution = 0;
			int day9Part2Solution = 0;

			var maxJ = input[0].Length;
			var maxI = input.Length;
			var grid = new int[maxI, maxJ];

			List<Day9GridPoint> LowPoints = new();
			List<int> basinSizes = new();

			for (int i = 0; i < maxI; i++)
			{
				for (int j = 0; j < maxJ; j++)
				{
					grid[i, j] = (int)input[i][j] - 48;
				}
			}

			//Part 1
			for (int i = 0; i < maxI; i++)
			{
				for (int j = 0; j < maxJ; j++)
				{
					//Up
					if (i - 1 >= 0)
					{
						if (grid[i, j] >= grid[(i - 1), j])
							continue;
					}

					//Right
					if (j + 1 < maxJ)
					{
						if (grid[i, j] >= grid[i, (j + 1)])
							continue;
					}

					//Down
					if (i + 1 < maxI)
					{
						if (grid[i, j] >= grid[(i + 1), j])
							continue;
					}

					//Left
					if (j - 1 >= 0)
					{
						if (grid[i, j] >= grid[i, (j - 1)])
							continue;
					}

					LowPoints.Add(new Day9GridPoint { I = i, J = j });
					day9Part1Solution += grid[i, j] + 1;

				}
			}

			//Part 2
			foreach (var item in LowPoints)
			{
				var thisBasinCount = 0;
				Queue<Day9GridPoint> nodesToVisit = new();
				List<Day9GridPoint> nodesVisited = new();
				nodesToVisit.Enqueue(item);
				while (nodesToVisit.Count > 0)
				{
					thisBasinCount++;
					var currentNode = nodesToVisit.Dequeue();
					nodesVisited.Add(currentNode);

					//Up
					if (currentNode.J - 1 >= 0 && grid[currentNode.I, (currentNode.J - 1)] != 9)
					{
						Day9GridPoint upPoint = new() { I = currentNode.I, J = (currentNode.J - 1) };
						if (!nodesToVisit.Contains(upPoint) && !nodesVisited.Contains(upPoint))
						{
							nodesToVisit.Enqueue(upPoint);
						}
					}

					//Right
					if (currentNode.I + 1 < maxI && grid[(currentNode.I + 1), currentNode.J] != 9)
					{
						Day9GridPoint rightPoint = new() { I = (currentNode.I + 1), J = currentNode.J };
						if (!nodesToVisit.Contains(rightPoint) && !nodesVisited.Contains(rightPoint))
						{
							nodesToVisit.Enqueue(rightPoint);
						}
					}

					//Down
					if (currentNode.J + 1 < maxJ && grid[currentNode.I, (currentNode.J + 1)] != 9)
					{
						Day9GridPoint downPoint = new() { I = currentNode.I, J = (currentNode.J + 1) };
						if (!nodesToVisit.Contains(downPoint) && !nodesVisited.Contains(downPoint))
						{
							nodesToVisit.Enqueue(downPoint);
						}
					}

					//Left
					if (currentNode.I - 1 >= 0 && grid[(currentNode.I - 1), currentNode.J] != 9)
					{
						Day9GridPoint leftPoint = new() { I = (currentNode.I - 1), J = currentNode.J };
						if (!nodesToVisit.Contains(leftPoint) && !nodesVisited.Contains(leftPoint))
						{
							nodesToVisit.Enqueue(leftPoint);
						}
					}

				}

				basinSizes.Add(thisBasinCount);
			}

			day9Part2Solution = basinSizes.OrderByDescending(x => x).Take(3).Aggregate(1, (x, y) => x * y);

			sw.Stop();
			Console.WriteLine($"Day 9, Part 1: {day9Part1Solution}");
			Console.WriteLine($"Day 9, Part 2: {day9Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public static void SolveDay10()
		{
			var input = File.ReadAllLines(@"input\10.txt");

			Stopwatch sw = new();
			sw.Start();

			List<long> incompleteScores = new();
			int day10Part1Solution = 0;

			//Part 1
			foreach (var item in input)
			{
				Stack<char> chunk = new();
				bool corrupt = false;

				for (int i = 0; i < item.Length; i++)
				{
					if (item[i] == '(' || item[i] == '[' || item[i] == '{' || item[i] == '<')
						chunk.Push(item[i]);
					else
					{
						if (item[i] == ')')
						{
							if (chunk.Peek() == '(')
								chunk.Pop();
							else
							{
								corrupt = true;
								day10Part1Solution += 3;
							}
						}
						if (item[i] == ']')
						{
							if (chunk.Peek() == '[')
								chunk.Pop();
							else
							{
								corrupt = true;
								day10Part1Solution += 57;
							}
						}
						if (item[i] == '}')
						{
							if (chunk.Peek() == '{')
								chunk.Pop();
							else
							{
								corrupt = true;
								day10Part1Solution += 1197;
							}
						}
						if (item[i] == '>')
						{
							if (chunk.Peek() == '<')
								chunk.Pop();
							else
							{
								corrupt = true;
								day10Part1Solution += 25137;
							}
						}
					}

					if (corrupt)
						break;

				}

				if (corrupt)
					continue;
				else if (chunk.Count > 0)
				{
					//Part 2
					long thisScore = 0;
					while (chunk.Count > 0)
					{
						thisScore *= 5;

						switch (chunk.Pop())
						{
							case '(':
								thisScore += 1;
								break;
							case '[':
								thisScore += 2;
								break;
							case '{':
								thisScore += 3;
								break;
							case '<':
								thisScore += 4;
								break;
							default:
								break;
						}
					}

					incompleteScores.Add(thisScore);
				}

			}

			incompleteScores = incompleteScores.OrderBy(x => x).ToList();
			var middleScoreIndex = incompleteScores.Count / 2;

			sw.Stop();
			Console.WriteLine($"Day 10, Part 1: {day10Part1Solution}");
			Console.WriteLine($"Day 10, Part 2: {incompleteScores[middleScoreIndex]}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public struct Day11OctPoint
		{
			public int I { get; set; }
			public int J { get; set; }
		}

		public static void SolveDay11()
		{
			var input = File.ReadAllLines(@"input\11.txt");

			Stopwatch sw = new();
			sw.Start();

			//Initial Setup
			int size = 10;
			int day11Part1Solution = 0;
			int day11Part2Solution = 0;
			int flashes = 0;
			var grid = new int[size, size];
			for (int i = 0; i < input.Length; i++)
			{
				var split = input[i].Select(x => int.Parse(x.ToString())).ToArray();
				for (int j = 0; j < split.Length; j++)
				{
					grid[i, j] = split[j];
				}
			}

			//Fudged this since we know which step we find all flashes
			//Parts 1 & 2
			for (int s = 1; s <= 800; s++)
			{
				int thisStepFlashes = 0;

				Queue<Day11OctPoint> toFlash = new();
				List<Day11OctPoint> alreadyFlashed = new();

				for (int i = 0; i < size; i++)
				{
					for (int j = 0; j < size; j++)
					{
						grid[i, j]++;

						if (grid[i, j] > 9)
						{
							toFlash.Enqueue(new Day11OctPoint() { I = i, J = j });
						}
					}
				}

				while (toFlash.Count > 0)
				{
					var thisGrid = toFlash.Dequeue();
					alreadyFlashed.Add(thisGrid);

					flashes++;
					thisStepFlashes++;

					if (thisGrid.J > 0)
					{
						// -1, -1
						if (thisGrid.I > 0)
						{
							var upLeft = new Day11OctPoint { I = thisGrid.I - 1, J = thisGrid.J - 1 };
							if (!toFlash.Contains(upLeft) && !alreadyFlashed.Contains(upLeft))
							{
								grid[upLeft.I, upLeft.J]++;
								if (grid[upLeft.I, upLeft.J] > 9)
									toFlash.Enqueue(upLeft);
							}
						}

						// 0, -1
						var up = new Day11OctPoint { I = thisGrid.I, J = thisGrid.J - 1 };
						if (!toFlash.Contains(up) && !alreadyFlashed.Contains(up))
						{
							grid[up.I, up.J]++;
							if (grid[up.I, up.J] > 9)
								toFlash.Enqueue(up);
						}

						// +1, -1
						if (thisGrid.I < (size - 1))
						{
							var upRight = new Day11OctPoint { I = thisGrid.I + 1, J = thisGrid.J - 1 };
							if (!toFlash.Contains(upRight) && !alreadyFlashed.Contains(upRight))
							{
								grid[upRight.I, upRight.J]++;
								if (grid[upRight.I, upRight.J] > 9)
									toFlash.Enqueue(upRight);
							}
						}
					}

					// -1, 0
					if (thisGrid.I > 0)
					{
						var left = new Day11OctPoint { I = thisGrid.I - 1, J = thisGrid.J };
						if (!toFlash.Contains(left) && !alreadyFlashed.Contains(left))
						{
							grid[left.I, left.J]++;
							if (grid[left.I, left.J] > 9)
								toFlash.Enqueue(left);
						}
					}

					// +1, 0
					if (thisGrid.I < (size - 1))
					{
						var right = new Day11OctPoint { I = thisGrid.I + 1, J = thisGrid.J };
						if (!toFlash.Contains(right) && !alreadyFlashed.Contains(right))
						{
							grid[right.I, right.J]++;
							if (grid[right.I, right.J] > 9)
								toFlash.Enqueue(right);
						}
					}

					if (thisGrid.J < (size - 1))
					{
						// -1, +1
						if (thisGrid.I > 0)
						{
							var downLeft = new Day11OctPoint { I = thisGrid.I - 1, J = thisGrid.J + 1 };
							if (!toFlash.Contains(downLeft) && !alreadyFlashed.Contains(downLeft))
							{
								grid[downLeft.I, downLeft.J]++;
								if (grid[downLeft.I, downLeft.J] > 9)
									toFlash.Enqueue(downLeft);
							}
						}

						// 0, +1
						var down = new Day11OctPoint { I = thisGrid.I, J = thisGrid.J + 1 };
						if (!toFlash.Contains(down) && !alreadyFlashed.Contains(down))
						{
							grid[down.I, down.J]++;
							if (grid[down.I, down.J] > 9)
								toFlash.Enqueue(down);
						}

						// +1, +1
						if (thisGrid.I < (size - 1))
						{
							var downRight = new Day11OctPoint { I = thisGrid.I + 1, J = thisGrid.J + 1 };
							if (!toFlash.Contains(downRight) && !alreadyFlashed.Contains(downRight))
							{
								grid[downRight.I, downRight.J]++;
								if (grid[downRight.I, downRight.J] > 9)
									toFlash.Enqueue(downRight);
							}
						}
					}

				}

				foreach (var item in alreadyFlashed)
				{
					grid[item.I, item.J] = 0;
				}

				if (s == 100)
				{
					day11Part1Solution = flashes;
				}

				if (thisStepFlashes == (size * size))
				{
					day11Part2Solution = s;
					break;
				}

			}

			sw.Stop();
			Console.WriteLine($"Day 11, Part 1: {day11Part1Solution}");
			Console.WriteLine($"Day 11, Part 2: {day11Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public struct Day12PathElement
		{
			public int Value { get; set; }
			public bool IsSmall { get; set; }

			public Day12PathElement(string input)
			{
				Value = Day12GetInputIntValue(input);

				if (Value > 0)
					IsSmall = char.IsLower(input[0]);
				else
					IsSmall = false;
			}
		}

		public struct Day12PathParts
		{
			public Day12PathElement Left { get; set; }
			public Day12PathElement Right { get; set; }

			public Day12PathParts(string[] input)
			{
				Left = new Day12PathElement(input[0]);
				Right = new Day12PathElement(input[1]);
			}
		}

		public static int Day12GetInputIntValue(string input)
		{
			if (input == "start")
				return 0;
			else if (input == "end")
				return -1;
			else
				return int.Parse(string.Join("", input.ToCharArray().Select(x => (int)x)));
		}

		public struct Day12Paths
		{
			public int[] SmallCavesVisited { get; set; }
			public int LastCaveVisited { get; set; }
		}

		public struct Day12Part2Paths
		{
			public Day12Paths Path;
			public bool SmallCaveVisitedTwice { get; set; }
		}

		public static void SolveDay12()
		{
			var input = File.ReadAllLines(@"input\12.txt");

			Stopwatch sw = new();
			sw.Start();

			int day12Part1Solution = 0;
			int day12Part2Solution = 0;

			List<Day12PathParts> paths = new();
			List<int> smallCaves = new();

			foreach (var item in input)
			{
				var splitInput = item.Split('-');
				var pathToAdd = new Day12PathParts(splitInput);
				paths.Add(pathToAdd);
				if (pathToAdd.Left.IsSmall && !smallCaves.Contains(pathToAdd.Left.Value))
					smallCaves.Add(pathToAdd.Left.Value);
				if (pathToAdd.Right.IsSmall && !smallCaves.Contains(pathToAdd.Right.Value))
					smallCaves.Add(pathToAdd.Right.Value);
			}

			Stack<Day12Paths> part1PathsToTravel = new();
			Stack<Day12Part2Paths> part2PathsToTravel = new();

			var startingLeft = paths.Where(x => x.Left.Value.Equals(0));
			foreach (var startingPoint in startingLeft)
			{
				var smallCaveVisited = new int[smallCaves.Count];
				if (startingPoint.Right.IsSmall)
					smallCaveVisited[0] = startingPoint.Right.Value;

				var path = new Day12Paths() { SmallCavesVisited = smallCaveVisited, LastCaveVisited = startingPoint.Right.Value };
				part1PathsToTravel.Push(path);
				part2PathsToTravel.Push(new Day12Part2Paths() { Path = path, SmallCaveVisitedTwice = false });
			}

			var startingRight = paths.Where(x => x.Right.Value.Equals(0));
			foreach (var startingPoint in startingRight)
			{
				var smallCaveVisited = new int[smallCaves.Count];
				if (startingPoint.Left.IsSmall)
					smallCaveVisited[0] = startingPoint.Left.Value;

				var path = new Day12Paths() { SmallCavesVisited = smallCaveVisited, LastCaveVisited = startingPoint.Left.Value };
				part1PathsToTravel.Push(path);
				part2PathsToTravel.Push(new Day12Part2Paths() { Path = path, SmallCaveVisitedTwice = false });
			}

			//Part 1
			while (part1PathsToTravel.Count > 0)
			{
				var thisPath = part1PathsToTravel.Pop();

				var leftBranches = paths.Where(x => x.Left.Value.Equals(thisPath.LastCaveVisited));
				foreach (var branch in leftBranches)
				{
					if (branch.Right.Value.Equals(0))
						continue;
					else if (branch.Right.Value.Equals(-1))
						day12Part1Solution++;
					else
					{
						if (branch.Right.IsSmall)
						{
							if (!thisPath.SmallCavesVisited.Contains(branch.Right.Value))
							{
								var newSmallCavesVisited = (int[])thisPath.SmallCavesVisited.Clone();
								newSmallCavesVisited[Array.IndexOf(thisPath.SmallCavesVisited, 0)] = branch.Right.Value;
								part1PathsToTravel.Push(new Day12Paths() { SmallCavesVisited = newSmallCavesVisited, LastCaveVisited = branch.Right.Value });
							}
						}
						else
						{
							part1PathsToTravel.Push(new Day12Paths() { SmallCavesVisited = thisPath.SmallCavesVisited, LastCaveVisited = branch.Right.Value });
						}
					}
				}

				var rightBranches = paths.Where(x => x.Right.Value.Equals(thisPath.LastCaveVisited));
				foreach (var branch in rightBranches)
				{
					if (branch.Left.Value.Equals(0))
						continue;
					else if (branch.Left.Value.Equals(-1))
						day12Part1Solution++;
					else
					{
						if (branch.Left.IsSmall)
						{
							if (!thisPath.SmallCavesVisited.Contains(branch.Left.Value))
							{
								var newSmallCavesVisited = (int[])thisPath.SmallCavesVisited.Clone();
								newSmallCavesVisited[Array.IndexOf(thisPath.SmallCavesVisited, 0)] = branch.Left.Value;
								part1PathsToTravel.Push(new Day12Paths() { SmallCavesVisited = newSmallCavesVisited, LastCaveVisited = branch.Left.Value });
							}
						}
						else
						{
							part1PathsToTravel.Push(new Day12Paths() { SmallCavesVisited = thisPath.SmallCavesVisited, LastCaveVisited = branch.Left.Value });
						}
					}
				}

			}

			//Part 2
			while (part2PathsToTravel.Count > 0)
			{
				var thisPath = part2PathsToTravel.Pop();

				var leftBranches = paths.Where(x => x.Left.Value.Equals(thisPath.Path.LastCaveVisited));
				foreach (var branch in leftBranches)
				{
					if (branch.Right.Value.Equals(0))
						continue;
					else if (branch.Right.Value.Equals(-1))
						day12Part2Solution++;
					else
					{
						if (branch.Right.IsSmall)
						{
							if (!thisPath.SmallCaveVisitedTwice)
							{
								if (thisPath.Path.SmallCavesVisited.Count(x => x.Equals(branch.Right.Value)) == 0)
								{
									var newSmallCavesVisited = (int[])thisPath.Path.SmallCavesVisited.Clone();
									newSmallCavesVisited[Array.IndexOf(thisPath.Path.SmallCavesVisited, 0)] = branch.Right.Value;
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = new Day12Paths() { SmallCavesVisited = newSmallCavesVisited, LastCaveVisited = branch.Right.Value }, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
								}
								else if (thisPath.Path.SmallCavesVisited.Count(x => x.Equals(branch.Right.Value)) == 1)
								{
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = new Day12Paths() { SmallCavesVisited = thisPath.Path.SmallCavesVisited, LastCaveVisited = branch.Right.Value }, SmallCaveVisitedTwice = true });
								}
							}
							else
							{
								if (!thisPath.Path.SmallCavesVisited.Contains(branch.Right.Value))
								{
									var newSmallCavesVisited = (int[])thisPath.Path.SmallCavesVisited.Clone();
									newSmallCavesVisited[Array.IndexOf(thisPath.Path.SmallCavesVisited, 0)] = branch.Right.Value;
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = new Day12Paths() { SmallCavesVisited = newSmallCavesVisited, LastCaveVisited = branch.Right.Value }, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
								}
							}
						}
						else
						{
							part2PathsToTravel.Push(new Day12Part2Paths() { Path = new Day12Paths() { SmallCavesVisited = thisPath.Path.SmallCavesVisited, LastCaveVisited = branch.Right.Value }, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
						}
					}
				}

				var rightBranches = paths.Where(x => x.Right.Value.Equals(thisPath.Path.LastCaveVisited));
				foreach (var branch in rightBranches)
				{
					if (branch.Left.Value.Equals(0))
						continue;
					else if (branch.Left.Value.Equals(-1))
						day12Part2Solution++;
					else
					{
						if (branch.Left.IsSmall)
						{
							if (!thisPath.SmallCaveVisitedTwice)
							{
								if (thisPath.Path.SmallCavesVisited.Count(x => x.Equals(branch.Left.Value)) == 0)
								{
									var newSmallCavesVisited = (int[])thisPath.Path.SmallCavesVisited.Clone();
									newSmallCavesVisited[Array.IndexOf(thisPath.Path.SmallCavesVisited, 0)] = branch.Left.Value;
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = new Day12Paths() { SmallCavesVisited = newSmallCavesVisited, LastCaveVisited = branch.Left.Value }, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
								}
								else if (thisPath.Path.SmallCavesVisited.Count(x => x.Equals(branch.Left.Value)) == 1)
								{
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = new Day12Paths() { SmallCavesVisited = thisPath.Path.SmallCavesVisited, LastCaveVisited = branch.Left.Value }, SmallCaveVisitedTwice = true });
								}
							}
							else
							{
								if (!thisPath.Path.SmallCavesVisited.Contains(branch.Left.Value))
								{
									var newSmallCavesVisited = (int[])thisPath.Path.SmallCavesVisited.Clone();
									newSmallCavesVisited[Array.IndexOf(thisPath.Path.SmallCavesVisited, 0)] = branch.Left.Value;
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = new Day12Paths() { SmallCavesVisited = newSmallCavesVisited, LastCaveVisited = branch.Left.Value }, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
								}
							}
						}
						else
						{
							part2PathsToTravel.Push(new Day12Part2Paths() { Path = new Day12Paths() { SmallCavesVisited = thisPath.Path.SmallCavesVisited, LastCaveVisited = branch.Left.Value }, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
						}
					}
				}

			}

			sw.Stop();
			Console.WriteLine($"Day 12, Part 1: {day12Part1Solution}");
			Console.WriteLine($"Day 12, Part 2: {day12Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public static void SolveDay13()
		{
			var input = File.ReadAllLines(@"input\13.txt");

			Stopwatch sw = new();
			sw.Start();

			var dots = input.Where(x => !x.StartsWith("fold") && x.Length > 0).Select(x => x.Split(',').Select(x => int.Parse(x)).ToArray());
			var commands = input.Where(x => x.StartsWith("fold")).ToArray();

			var yMax = dots.Select(x => x[1]).Max();
			var xMax = dots.Select(x => x[0]).Max();

			var grid = new bool[(yMax + 1), (xMax + 1)];
			foreach (var item in dots)
			{
				grid[item[1], item[0]] = true;
			}

			int day13Part1Solution = 0;

			for (int c = 0; c < commands.Length; c++)
			{
				var command = commands[c].Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();
				var splitCommand = command.Split('=');
				var splitCommandValue = int.Parse(splitCommand[1]);

				switch (splitCommand[0])
				{
					case "y":
						{
							var newGrid = new bool[splitCommandValue, grid.GetLength(1)];
							for (int i = 0; i < splitCommandValue; i++)
							{
								for (int j = 0; j < grid.GetLength(1); j++)
								{
									newGrid[i, j] = grid[i, j] | grid[grid.GetLength(0) - i - 1, j];
								}
							}
							grid = newGrid;
						}
						break;
					case "x":
						{
							var newGrid = new bool[grid.GetLength(0), splitCommandValue];
							for (int i = 0; i < grid.GetLength(0); i++)
							{
								for (int j = 0; j < splitCommandValue; j++)
								{
									newGrid[i, j] = grid[i, j] | grid[i, grid.GetLength(1) - j - 1];
								}
							}
							grid = newGrid;
						}
						break;
					default:
						break;
				}

				//Part 1
				if (c == 0)
				{
					for (int i = 0; i < grid.GetLength(0); i++)
					{
						for (int j = 0; j < grid.GetLength(1); j++)
						{
							if (grid[i, j])
								day13Part1Solution++;
						}
					}
				}
			}

			sw.Stop();
			Console.WriteLine($"Day 13, Part 1: {day13Part1Solution}");
			Console.WriteLine($"Day 13, Part 2:");

			Console.WriteLine();
			for (int i = 0; i < grid.GetLength(0); i++)
			{
				for (int j = 0; j < grid.GetLength(1); j++)
				{
					if (grid[i, j])
						Console.Write("#");
					else
						Console.Write(" ");
				}
				Console.WriteLine();
			}
			Console.WriteLine();

			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public static void SolveDay14()
		{
			var input = File.ReadAllLines(@"input\14.txt");

			Stopwatch sw = new();
			sw.Start();

			var template = input[0];
			var pairInsertion = input.Skip(2).Select(x => x.Split(" -> ")).ToDictionary(x => x[0], x => x[1]);

			Dictionary<string, long> pairCounts = new();
			for (int i = 0; i < template.Length - 1; i++)
			{
				var key = template.Substring(i, 2);
				if (pairCounts.ContainsKey(key))
					pairCounts[key]++;
				else
					pairCounts.Add(key, 1);
			}

			long day14Part1Solution = 0;
			long day14Part2Solution = 0;

			//Parts 1 & 2
			for (int i = 0; i < 40; i++)
			{
				Dictionary<string, long> tempPairCounts = new();
				foreach (var item in pairCounts)
				{
					var leftSide = item.Key[0] + pairInsertion[item.Key];
					if (tempPairCounts.ContainsKey(leftSide))
						tempPairCounts[leftSide] += item.Value;
					else
						tempPairCounts.Add(leftSide, item.Value);

					var rightSide = pairInsertion[item.Key] + item.Key[1];
					if (tempPairCounts.ContainsKey(rightSide))
						tempPairCounts[rightSide] += item.Value;
					else
						tempPairCounts.Add(rightSide, item.Value);
				}
				pairCounts = tempPairCounts;

				if (i == 9)
				{
					var part1ElementCount = pairCounts.Select(x => new { Element = x.Key[0], Count = x.Value }).GroupBy(x => x.Element).ToDictionary(x => x.Key, x => x.Sum(x => x.Count));

					//Last element never matches any pairs but needs to be counted
					part1ElementCount[template.Last()]++;

					day14Part1Solution = part1ElementCount.Values.Max() - part1ElementCount.Values.Min();
				}

			}

			var part2ElementCount = pairCounts.Select(x => new { Element = x.Key[0], Count = x.Value }).GroupBy(x => x.Element).ToDictionary(x => x.Key, x => x.Sum(x => x.Count));

			//Last element never matches any pairs but needs to be counted
			part2ElementCount[template.Last()]++;

			day14Part2Solution = part2ElementCount.Values.Max() - part2ElementCount.Values.Min();

			sw.Stop();
			Console.WriteLine($"Day 14, Part 1: {day14Part1Solution}");
			Console.WriteLine($"Day 14, Part 2: {day14Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public static int Day15FindShortestPath(int[,] grid)
		{
			int[,] weight = new int[grid.GetLength(0), grid.GetLength(1)];
			for (int i = 0; i < weight.GetLength(0); i++)
			{
				for (int j = 0; j < weight.GetLength(1); j++)
				{
					weight[i, j] = int.MaxValue;
				}
			}

			Queue<Tuple<int, int>> tuples = new();
			tuples.Enqueue(new Tuple<int, int>(0, 0));
			weight[0, 0] = 0;
			while (tuples.Count > 0)
			{
				var thisTuple = tuples.Dequeue();

				//UP
				if (thisTuple.Item1 > 0)
				{
					var newWeight = weight[thisTuple.Item1, thisTuple.Item2] + grid[thisTuple.Item1 - 1, thisTuple.Item2];
					if (newWeight < weight[thisTuple.Item1 - 1, thisTuple.Item2])
					{
						weight[thisTuple.Item1 - 1, thisTuple.Item2] = newWeight;
						tuples.Enqueue(new Tuple<int, int>(thisTuple.Item1 - 1, thisTuple.Item2));
					}
				}

				//LEFT
				if (thisTuple.Item2 > 0)
				{
					var newWeight = weight[thisTuple.Item1, thisTuple.Item2] + grid[thisTuple.Item1, thisTuple.Item2 - 1];
					if (newWeight < weight[thisTuple.Item1, thisTuple.Item2 - 1])
					{
						weight[thisTuple.Item1, thisTuple.Item2 - 1] = newWeight;
						tuples.Enqueue(new Tuple<int, int>(thisTuple.Item1, thisTuple.Item2 - 1));
					}
				}

				//RIGHT
				if (thisTuple.Item2 < (grid.GetLength(1) - 1))
				{
					var newWeight = weight[thisTuple.Item1, thisTuple.Item2] + grid[thisTuple.Item1, thisTuple.Item2 + 1];
					if (newWeight < weight[thisTuple.Item1, thisTuple.Item2 + 1])
					{
						weight[thisTuple.Item1, thisTuple.Item2 + 1] = newWeight;
						tuples.Enqueue(new Tuple<int, int>(thisTuple.Item1, thisTuple.Item2 + 1));
					}
				}

				//DOWN
				if (thisTuple.Item1 < (grid.GetLength(0) - 1))
				{
					var newWeight = weight[thisTuple.Item1, thisTuple.Item2] + grid[thisTuple.Item1 + 1, thisTuple.Item2];
					if (newWeight < weight[thisTuple.Item1 + 1, thisTuple.Item2])
					{
						weight[thisTuple.Item1 + 1, thisTuple.Item2] = newWeight;
						tuples.Enqueue(new Tuple<int, int>(thisTuple.Item1 + 1, thisTuple.Item2));
					}
				}
			}

			return weight[weight.GetLength(0) - 1, weight.GetLength(1) - 1];
		}

		public static void SolveDay15()
		{
			var input = File.ReadAllLines(@"input\15.txt");

			Stopwatch sw = new();
			sw.Start();

			int s = input.Length;
			int[,] part1Grid = new int[s, s];
			int[,] part2Grid = new int[s * 5, s * 5];

			for (int i = 0; i < s; i++)
			{
				for (int j = 0; j < s; j++)
				{
					int value = (int)input[i][j] - 48;
					part1Grid[i + (0 * s), j + (0 * s)] = value;
					part2Grid[i + (0 * s), j + (0 * s)] = value;

					//9
					value++;
					if (value > 9)
						value = 1;

					part2Grid[i + (1 * s), j + (0 * s)] = value;
					part2Grid[i + (0 * s), j + (1 * s)] = value;

					//1
					value++;
					if (value > 9)
						value = 1;

					part2Grid[i + (2 * s), j + (0 * s)] = value;
					part2Grid[i + (1 * s), j + (1 * s)] = value;
					part2Grid[i + (0 * s), j + (2 * s)] = value;

					//2
					value++;
					if (value > 9)
						value = 1;

					part2Grid[i + (3 * s), j + (0 * s)] = value;
					part2Grid[i + (2 * s), j + (1 * s)] = value;
					part2Grid[i + (1 * s), j + (2 * s)] = value;
					part2Grid[i + (0 * s), j + (3 * s)] = value;

					//3
					value++;
					if (value > 9)
						value = 1;

					part2Grid[i + (4 * s), j + (0 * s)] = value;
					part2Grid[i + (3 * s), j + (1 * s)] = value;
					part2Grid[i + (2 * s), j + (2 * s)] = value;
					part2Grid[i + (1 * s), j + (3 * s)] = value;
					part2Grid[i + (0 * s), j + (4 * s)] = value;

					//4
					value++;
					if (value > 9)
						value = 1;

					part2Grid[i + (4 * s), j + (1 * s)] = value;
					part2Grid[i + (3 * s), j + (2 * s)] = value;
					part2Grid[i + (2 * s), j + (3 * s)] = value;
					part2Grid[i + (1 * s), j + (4 * s)] = value;

					//5
					value++;
					if (value > 9)
						value = 1;

					part2Grid[i + (4 * s), j + (2 * s)] = value;
					part2Grid[i + (3 * s), j + (3 * s)] = value;
					part2Grid[i + (2 * s), j + (4 * s)] = value;

					//6
					value++;
					if (value > 9)
						value = 1;

					part2Grid[i + (4 * s), j + (3 * s)] = value;
					part2Grid[i + (3 * s), j + (4 * s)] = value;

					//7
					value++;
					if (value > 9)
						value = 1;

					part2Grid[i + (4 * s), j + (4 * s)] = value;
				}
			}

			//Part 1
			int day15Part1Solution = Day15FindShortestPath(part1Grid);

			//Part 2
			int day15Part2Solution = Day15FindShortestPath(part2Grid);

			sw.Stop();
			Console.WriteLine($"Day 15, Part 1: {day15Part1Solution}");
			Console.WriteLine($"Day 15, Part 2: {day15Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public class Day16
		{
			public string binaryInput;
			public int index;
			public int totalVersionNumber;

			public void Solve()
			{
				var input = File.ReadAllLines(@"input\16.txt");

				Stopwatch sw = new();
				sw.Start();

				binaryInput = string.Join(string.Empty, input[0].Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
				totalVersionNumber = 0;
				index = 0;

				var day16Part2Solution = Process();

				sw.Stop();
				Console.WriteLine($"Day 16, Part 1: {totalVersionNumber}");
				Console.WriteLine($"Day 15, Part 2: {day16Part2Solution}");
				Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
			}

			public long Process()
			{
				var packetVersion = Convert.ToInt32(binaryInput.Substring(index, 3), 2);
				totalVersionNumber += packetVersion;
				index += 3;

				var packetType = Convert.ToInt32(binaryInput.Substring(index, 3), 2);
				index += 3;

				switch (packetType)
				{
					case 0:
						return ProcessOperator().Sum();
					case 1:
						return ProcessOperator().Aggregate(1L, (x, y) => x * y);
					case 2:
						return ProcessOperator().Min();
					case 3:
						return ProcessOperator().Max();
					case 4:
						return ProcessLiteral();
					case 5:
						var gtValues = ProcessOperator();
						return gtValues[0] > gtValues[1] ? 1 : 0;
					case 6:
						var ltValues = ProcessOperator();
						return ltValues[0] < ltValues[1] ? 1 : 0;
					case 7:
						var eqValues = ProcessOperator();
						return eqValues[0] == eqValues[1] ? 1 : 0;
					default:
						return 0;
				}
			}

			public long ProcessLiteral()
			{
				bool finalDigit = false;
				string literal = string.Empty;
				while (!finalDigit)
				{
					var prefix = binaryInput[index];
					index++;

					literal += binaryInput.Substring(index, 4);
					index += 4;

					if (prefix == '0')
						finalDigit = true;
				}

				return Convert.ToInt64(literal, 2);
			}

			public List<long> ProcessOperator()
			{
				List<long> values = new();

				var lengthTypeID = Convert.ToInt32(binaryInput.Substring(index, 1), 2);
				index++;

				switch (lengthTypeID)
				{
					case 0:
						var bits = Convert.ToInt32(binaryInput.Substring(index, 15), 2);
						index += 15;

						var indexTarget = index + bits;
						while (index < indexTarget)
						{
							values.Add(Process());
						}

						return values;
					case 1:
						var packets = Convert.ToInt32(binaryInput.Substring(index, 11), 2);
						index += 11;

						for (int i = 0; i < packets; i++)
						{
							values.Add(Process());
						}

						return values;
					default:
						return values;
				}
			}
		}

		public static void SolveDay17()
		{
			var input = File.ReadAllLines(@"input\17.txt");

			Stopwatch sw = new();
			sw.Start();

			var split = input[0].Replace(",", "").Split(' ');
			var xValues = split[2].Split("=")[1].Split("..").Select(x => int.Parse(x));
			var yValues = split[3].Split("=")[1].Split("..").Select(x => int.Parse(x));

			var minXValue = xValues.Min();
			var maxXValue = xValues.Max();
			var minYValue = yValues.Min();
			var maxYValue = yValues.Max();

			var day17Part1Solution = int.MinValue;
			var day17Part2Solution = 0;

			for (int y = minYValue; y <= (-1 * minYValue); y++)
			{
				for (int x = 0; x <= maxXValue; x++)
				{
					var thisVeloMaxY = int.MinValue;
					int thisXVelo = x;
					int thisYVelo = y;
					int thisXPos = 0;
					int thisYPos = 0;

					while (true)
					{
						thisXPos += thisXVelo;
						thisYPos += thisYVelo;

						//gravity
						thisYVelo--;

						//drag
						if (thisXVelo > 0)
							thisXVelo--;
						else if (thisXVelo < 0)
							thisXVelo++;

						//track highest y value
						if (thisYPos > thisVeloMaxY)
						{
							thisVeloMaxY = thisYPos;
						}

						if (thisXPos >= minXValue && thisXPos <= maxXValue && thisYPos >= minYValue && thisYPos <= maxYValue)
						{
							//VALID
							day17Part2Solution++;

							if (thisVeloMaxY > day17Part1Solution)
								day17Part1Solution = thisVeloMaxY;

							break;
						}
						else
						{
							if ((thisXVelo == 0 && (thisXPos < minXValue || thisXPos > maxXValue)) || thisYPos < minYValue)
							{
								//IMPOSSIBLE
								break;
							}
						}
					}

				}
			}

			sw.Stop();
			Console.WriteLine($"Day 17, Part 1: {day17Part1Solution}");
			Console.WriteLine($"Day 17, Part 2: {day17Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public struct Day18ExplodedValues
		{
			public int LeftValue { get; set; }
			public int RightValue { get; set; }
		}

		public class Day18SnailfishNumber
		{
			public Day18SnailfishNumber Left { get; set; }
			public Day18SnailfishNumber Right { get; set; }
			public int Value { get; set; }

			public bool NumericValue()
			{
				return Left == null && Right == null;
			}

			public int Magnitude()
			{
				if (NumericValue())
					return Value;

				return ((3 * Left.Magnitude()) + (2 * Right.Magnitude()));
			}

			public static Day18SnailfishNumber Create(string input, ref int index)
			{
				Day18SnailfishNumber newSnailfishNumber = new();
				if (char.IsDigit(input[index]))
				{
					newSnailfishNumber.Value = int.Parse(input[index].ToString());
					index++;
					return newSnailfishNumber;
				}
				else
				{
					index++;
					newSnailfishNumber.Left = Create(input, ref index);
					index++;
					newSnailfishNumber.Right = Create(input, ref index);
					index++;
					return newSnailfishNumber;
				}
			}

			public void Add(Day18SnailfishNumber secondNumber)
			{
				Day18SnailfishNumber newSnailfishNumber = new() { Left = Left, Right = Right };
				Left = newSnailfishNumber;
				Right = secondNumber.Clone();
				Reduce();
			}

			public Day18SnailfishNumber Clone()
			{
				return new Day18SnailfishNumber()
				{
					Left = Left?.Clone(),
					Right = Right?.Clone(),
					Value = Value
				};
			}

			public void Reduce()
			{
				while (true)
				{
					Explode();
					if (Split())
						continue;
					break;
				}
			}

			public Day18ExplodedValues Explode(int depth = 0)
			{
				if (NumericValue())
					return new Day18ExplodedValues();

				if (depth >= 4 && Left != null && Left.NumericValue() && Right != null && Right.NumericValue())
				{
					//Explode if at depth 4+ and in a "natural pair"
					var returnValues = new Day18ExplodedValues() { LeftValue = Left.Value, RightValue = Right.Value };
					Left = null;
					Right = null;
					Value = 0;
					return returnValues;
				}
				else
				{
					var explodedLeft = Left.Explode(depth + 1);
					if (explodedLeft.RightValue > 0)
					{
						var rightBranch = Right;
						while (!rightBranch.NumericValue())
						{
							rightBranch = rightBranch.Left;
						}
						rightBranch.Value += explodedLeft.RightValue;
					}

					var explodedRight = Right.Explode(depth + 1);
					if (explodedRight.LeftValue > 0)
					{
						var leftBranch = Left;
						while (!leftBranch.NumericValue())
						{
							leftBranch = leftBranch.Right;
						}
						leftBranch.Value += explodedRight.LeftValue;
					}

					return new Day18ExplodedValues() { LeftValue = explodedLeft.LeftValue, RightValue = explodedRight.RightValue };
				}
			}

			public bool Split()
			{
				if (Value >= 10 && NumericValue())
				{
					//Split if the value is greater than or equal to 10
					Left = new Day18SnailfishNumber() { Value = Value / 2 };
					Right = new Day18SnailfishNumber() { Value = (Value + 1) / 2 };

					return true;
				}
				else
				{
					if (Left != null && Left.Split())
						return true;

					if (Right != null && Right.Split())
						return true;

					return false;
				}
			}

			public override string ToString()
			{
				if (NumericValue())
					return Value.ToString();

				return $"[{Left},{Right}]";
			}

			public static Day18SnailfishNumber AddCreateNew(Day18SnailfishNumber firstNumber, Day18SnailfishNumber secondNumber)
			{
				Day18SnailfishNumber newSnailfishNumber = new() { Left = firstNumber.Clone(), Right = secondNumber.Clone() };
				newSnailfishNumber.Reduce();
				return newSnailfishNumber;
			}
		}

		public static void SolveDay18()
		{
			var input = File.ReadAllLines(@"input\18.txt");

			var sw = new Stopwatch();
			sw.Start();

			List<Day18SnailfishNumber> numbers = new();
			foreach (var item in input)
			{
				int index = 0;
				var newSnailfishNumber = Day18SnailfishNumber.Create(item, ref index);
				numbers.Add(newSnailfishNumber);
			}

			//Part 1
			var originalNumberZero = numbers[0].Clone();
			for (int i = 1; i < numbers.Count; i++)
			{
				numbers[0].Add(numbers[i]);
			}
			var day18Part1Solution = numbers[0].Magnitude();
			numbers[0] = originalNumberZero;

			//Part 2
			var day18Part2Solution = int.MinValue;
			for (int i = 0; i < numbers.Count; i++)
			{
				for (int j = 0; j < numbers.Count; j++)
				{
					if (i == j)
						continue;

					var snailfishIPlusJ = Day18SnailfishNumber.AddCreateNew(numbers[i], numbers[j]);
					var snailfishIPlusJMagnitude = snailfishIPlusJ.Magnitude();
					if (snailfishIPlusJMagnitude > day18Part2Solution)
					{
						day18Part2Solution = snailfishIPlusJMagnitude;
					}

					var snailfishJPlusI = Day18SnailfishNumber.AddCreateNew(numbers[j], numbers[i]);
					var snailfishJPlusIMagnitude = snailfishJPlusI.Magnitude();
					if (snailfishJPlusIMagnitude > day18Part2Solution)
					{
						day18Part2Solution = snailfishJPlusIMagnitude;
					}
				}
			}

			sw.Stop();
			Console.WriteLine($"Day 18, Part 1: {day18Part1Solution}");
			Console.WriteLine($"Day 18, Part 2: {day18Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public static void SolveDay19()
		{
			var input = File.ReadAllLines(@"input\19.txt");

			Stopwatch sw = new();
			sw.Start();

			List<Day19Scanner> scanners = new();
			Day19Scanner newScanner = new();
			foreach (var item in input)
			{
				if (item.Contains("---"))
				{
					newScanner.Id = int.Parse(item.Split(" ")[2]);
				}
				else if (item.Length == 0)
				{
					scanners.Add(newScanner);
					newScanner = new();
				}
				else
				{
					var splitValues = item.Split(',').Select(x => int.Parse(x)).ToArray();
					newScanner.Beacons.Add(new Vector3(splitValues[0], splitValues[1], splitValues[2]));
				}
			}
			scanners.Add(newScanner);

			List<Day19Scanner> processedScanners = new() { scanners[0] };
			HashSet<Vector3> locatedBeacons = scanners[0].Beacons.ToHashSet();
			Queue<Day19Scanner> scannersToProcess = new();
			for (int i = 1; i < scanners.Count; i++)
				scannersToProcess.Enqueue(scanners[i]);

			while (scannersToProcess.Count > 0)
			{
				var scannerToCheck = scannersToProcess.Dequeue();
				var scannerLocated = false;
				foreach (var transformation in Day19Transforms)
				{
					//Transform the current beacons
					var transformedBeacons = scannerToCheck.Beacons.Select(transformation).ToList();

					//Find the difference of these beacons to ones we know the location of
					var offsetBeaconsFromLocated = transformedBeacons.SelectMany(x => locatedBeacons.Select(y => x - y));

					//Find if we have 12+ beacons with the same offset
					var offsetMatched = offsetBeaconsFromLocated.GroupBy(x => x).Where(x => x.Count() >= 12).Select(x => (Vector: x.Key, Count: x.Count()));

					if (offsetMatched != null && offsetMatched.Count() > 0)
					{
						var offsetVector = offsetMatched.ElementAt(0).Vector;
						
						//Add the newly located beacons to our set
						foreach (var beacon in transformedBeacons)
						{
							locatedBeacons.Add(beacon - offsetVector);
						}

						//Store the scanner location for Part 2
						scannerToCheck.ScannerLocation = offsetVector;
						processedScanners.Add(scannerToCheck);

						scannerLocated = true;
					}
				}

				//This scanner could not be located currently, try again later
				if (!scannerLocated)
					scannersToProcess.Enqueue(scannerToCheck);
			}

			//Part 2
			int maxManhattanDistance = int.MinValue;

			for (int i = 0; i < processedScanners.Count; i++)
			{
				for (int j = (i + 1); j < processedScanners.Count; j++)
				{
					var thisManhattanDistance =
						(int)Math.Abs(processedScanners[i].ScannerLocation.X - processedScanners[j].ScannerLocation.X)
						+
						(int)Math.Abs(processedScanners[i].ScannerLocation.Y - processedScanners[j].ScannerLocation.Y)
						+
						(int)Math.Abs(processedScanners[i].ScannerLocation.Z - processedScanners[j].ScannerLocation.Z);

					if (thisManhattanDistance > maxManhattanDistance)
					{
						maxManhattanDistance = thisManhattanDistance;
					}
				}
			}

			sw.Stop();
			Console.WriteLine($"Day 19, Part 1: {locatedBeacons.Count}");
			Console.WriteLine($"Day 19, Part 2: {maxManhattanDistance}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public static Func<Vector3, Vector3>[] Day19Transforms = new Func<Vector3, Vector3>[]
		{
			vectorIn => new(vectorIn.X, vectorIn.Y, vectorIn.Z),
			vectorIn => new(vectorIn.X, vectorIn.Z, -vectorIn.Y),
			vectorIn => new(vectorIn.X, -vectorIn.Y, -vectorIn.Z),
			vectorIn => new(vectorIn.X, -vectorIn.Z, vectorIn.Y),
			vectorIn => new(-vectorIn.X, vectorIn.Y, -vectorIn.Z),
			vectorIn => new(-vectorIn.X, vectorIn.Z, vectorIn.Y),
			vectorIn => new(-vectorIn.X, -vectorIn.Y, vectorIn.Z),
			vectorIn => new(-vectorIn.X, -vectorIn.Z, -vectorIn.Y),

			vectorIn => new(vectorIn.Y, vectorIn.X, -vectorIn.Z),
			vectorIn => new(vectorIn.Y, vectorIn.Z, vectorIn.X),
			vectorIn => new(vectorIn.Y, -vectorIn.X, vectorIn.Z),
			vectorIn => new(vectorIn.Y, -vectorIn.Z, -vectorIn.X),
			vectorIn => new(-vectorIn.Y, vectorIn.X, vectorIn.Z),
			vectorIn => new(-vectorIn.Y, vectorIn.Z, -vectorIn.X),
			vectorIn => new(-vectorIn.Y, -vectorIn.X, -vectorIn.Z),
			vectorIn => new(-vectorIn.Y, -vectorIn.Z, vectorIn.X),

			vectorIn => new(vectorIn.Z, vectorIn.X, vectorIn.Y),
			vectorIn => new(vectorIn.Z, vectorIn.Y, -vectorIn.X),
			vectorIn => new(vectorIn.Z, -vectorIn.X, -vectorIn.Y),
			vectorIn => new(vectorIn.Z, -vectorIn.Y, vectorIn.X),
			vectorIn => new(-vectorIn.Z, vectorIn.X, -vectorIn.Y),
			vectorIn => new(-vectorIn.Z, vectorIn.Y, vectorIn.X),
			vectorIn => new(-vectorIn.Z, -vectorIn.X, vectorIn.Y),
			vectorIn => new(-vectorIn.Z, -vectorIn.Y, -vectorIn.X)
		};

		public class Day19Scanner
		{
			public int Id { get; set; }
			public List<Vector3> Beacons { get; set; } = new();
			public Vector3 ScannerLocation { get; set; } = new();
		}

		public static void SolveDay20()
		{
			var input = File.ReadAllLines(@"input\20.txt");

			Stopwatch sw = new();
			sw.Start();

			int day20Part1Solution = 0;
			int day20Part2Solution = 0;

			var algorithm = input[0].Select(x => x == '#').ToArray();
			int size = input[2].Length;

			bool AllOffState = algorithm[0];
			bool AllOnState = algorithm[511];
			bool infiniteState = AllOffState ? AllOnState : AllOffState;

			HashSet<(int, int)> grid = new();
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
					if (input[i + 2][j] == '#')
						grid.Add((i, j));


			for (int step = 0; step < 50; step++)
			{
				HashSet<(int, int)> newGrid = new();
				var minI = grid.Min(x => x.Item1);
				var maxI = grid.Max(x => x.Item1);
				var minJ = grid.Min(x => x.Item2);
				var maxJ = grid.Max(x => x.Item2);
				for (int i = minI - 1; i <= maxI + 1; i++)
				{
					for (int j = minJ - 1; j <= maxJ + 1; j++)
					{
						var bitValues = new BitArray(new bool[]
						{
							(infiniteState && (i+1 > maxI || i+1 < minI || j+1 > maxJ || j+1 < minJ)) || grid.Contains((i+1,j+1)),
							(infiniteState && (i+1 > maxI || i+1 < minI || j > maxJ || j < minJ)) || grid.Contains((i+1,j)),
							(infiniteState && (i+1 > maxI || i+1 < minI || j-1 > maxJ || j-1 < minJ)) || grid.Contains((i+1,j-1)),

							(infiniteState && (i > maxI || i < minI || j+1 > maxJ || j+1 < minJ)) || grid.Contains((i,j+1)),
							(infiniteState && (i > maxI || i < minI || j > maxJ || j < minJ)) || grid.Contains((i,j)),
							(infiniteState && (i > maxI || i < minI || j-1 > maxJ || j-1 < minJ)) || grid.Contains((i,j-1)),

							(infiniteState && (i-1 > maxI || i-1 < minI || j+1 > maxJ || j+1 < minJ)) || grid.Contains((i-1,j+1)),
							(infiniteState && (i-1 > maxI || i-1 < minI || j > maxJ || j < minJ)) || grid.Contains((i-1,j)),
							(infiniteState && (i-1 > maxI || i-1 < minI || j-1 > maxJ || j-1 < minJ)) || grid.Contains((i-1,j-1)),
						});
						var value = new int[1];
						bitValues.CopyTo(value, 0);

						if (algorithm[value[0]])
							newGrid.Add((i, j));
					}
				}
				grid = newGrid;
				infiniteState = infiniteState ? AllOnState : AllOffState;
				if (step == 1)
				{
					day20Part1Solution = grid.Count();
				}
			}
			day20Part2Solution = grid.Count();

			sw.Stop();
			Console.WriteLine($"Day 20, Part 1: {day20Part1Solution}");
			Console.WriteLine($"Day 20, Part 2: {day20Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}

		public class Day21
		{
			public static int deterministicDieRolls = 0;
			public static int deterministicDieValue = 100;
			public static int Day21RollDeterministicDie()
			{
				deterministicDieRolls++;
				deterministicDieValue = (deterministicDieValue) % 100 + 1;
				return deterministicDieValue;
			}

			public static Dictionary<int, int> DiracDiceRolls =>
				new()
				{
					{ 3, 1 },   //(1,1,1)
					{ 4, 3 },   //(1,1,2)
					{ 5, 6 },   //(1,2,2) || (1,1,3)
					{ 6, 7 },   //(1,2,3) || (2,2,2)
					{ 7, 6 },   //(2,2,3) || (1,3,3)
					{ 8, 3 },   //(2,3,3)
					{ 9, 1 },   //(3,3,3)
				};

			public struct Day21Player
			{
				public int Score { get; set; }
				public int Pos { get; set; }

				public void AddDieRoll(int DieValue)
				{
					Pos = (Pos + DieValue - 1) % 10 + 1;
					Score += Pos;
				}

				public override string ToString()
				{
					return $"{Pos} - {Score}";
				}
			}

			public void Solve()
			{
				Stopwatch sw = new();
				sw.Start();

				long day21Part1Solution;
				long day21Part2Solution;

				int player1StartingPos = 4;
				int player2StartingPos = 7;

				var part1 = (
					new Day21Player() { Pos = player1StartingPos },
					new Day21Player() { Pos = player2StartingPos }
				);

				//Part 1
				while (true)
				{
					part1.Item1.AddDieRoll(Day21RollDeterministicDie() + Day21RollDeterministicDie() + Day21RollDeterministicDie());
					if (part1.Item1.Score >= 1000)
					{
						day21Part1Solution = deterministicDieRolls * part1.Item2.Score;
						break;
					}

					part1.Item2.AddDieRoll(Day21RollDeterministicDie() + Day21RollDeterministicDie() + Day21RollDeterministicDie());
					if (part1.Item2.Score >= 1000)
					{
						day21Part1Solution = deterministicDieRolls * part1.Item1.Score;
						break;
					}
				}

				//Part 2
				long p1UniverseWins = 0;
				long p2UniverseWins = 0;

				Dictionary<(Day21Player, Day21Player), long> allUniverses = new();
				allUniverses.Add((new Day21Player() { Pos = player1StartingPos }, new Day21Player() { Pos = player2StartingPos }), 1);

				while (allUniverses.Count > 0)
				{
					Dictionary<(Day21Player, Day21Player), long> newUniverses = new();

					//For every previous universe state, let Player 1 & Player 2 take a turn
					foreach (var universeState in allUniverses)
					{
						//For every roll combo/count for Player 1
						foreach (var p1DieRoll in DiracDiceRolls)
						{
							var player1 = new Day21Player() { Pos = universeState.Key.Item1.Pos, Score = universeState.Key.Item1.Score };
							player1.AddDieRoll(p1DieRoll.Key);
							if (player1.Score >= 21)
							{
								//Player 1 wins don't count whatever Player 2 would have rolled
								p1UniverseWins += universeState.Value * p1DieRoll.Value;
								continue;
							}

							//For every roll combo/count for Player 2
							foreach (var p2DieRoll in DiracDiceRolls)
							{
								var player2 = new Day21Player() { Pos = universeState.Key.Item2.Pos, Score = universeState.Key.Item2.Score };
								player2.AddDieRoll(p2DieRoll.Key);
								long universeCount = universeState.Value * p1DieRoll.Value * p2DieRoll.Value;
								if (player2.Score >= 21)
								{
									//Player 2 wins have to include the rolls Player 1 made to get us to this state
									p2UniverseWins += universeCount;
									continue;
								}

								if (newUniverses.ContainsKey((player1, player2)))
								{
									newUniverses[(player1, player2)] += universeCount;
								}
								else
								{
									newUniverses.Add((player1, player2), universeCount);
								}
							}
						}
					}

					allUniverses = newUniverses;
				}

				day21Part2Solution = p1UniverseWins > p2UniverseWins ? p1UniverseWins : p2UniverseWins;

				sw.Stop();
				Console.WriteLine($"Day 21, Part 1: {day21Part1Solution}");
				Console.WriteLine($"Day 21, Part 2: {day21Part2Solution}");
				Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
			}
		}
	}
}