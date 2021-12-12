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
			SolveDay12();
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
				lookup.Add(tenDigits[0], 1);	//Digit 1 is our two char Digit
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
					if (currentNode.J - 1 >= 0 && grid[currentNode.I,(currentNode.J-1)] != 9)
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

		public struct Day12Part2Paths
		{
			public List<int> Path;
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
			List<string> elements = new();

			foreach (var item in input)
			{
				var splitInput = item.Split('-');
				paths.Add(new Day12PathParts(splitInput));
				elements.AddRange(splitInput);
			}

			//For Output Uses Only
			Dictionary<int, string> outputLookup = elements.Distinct().ToDictionary(x => Day12GetInputIntValue(x), x => x);

			Stack<List<int>> part1PathsToTravel = new();
			List<List<int>> part1PathsTraveled = new();

			Stack<Day12Part2Paths> part2PathsToTravel = new();
			List<List<int>> part2PathsTraveled = new();

			var startingLeft = paths.Where(x => x.Left.Value.Equals(0));
			foreach (var startingPoint in startingLeft)
			{
				part1PathsToTravel.Push(new List<int>() { startingPoint.Left.Value, startingPoint.Right.Value });
				part2PathsToTravel.Push(new Day12Part2Paths() { Path = new List<int>() { startingPoint.Left.Value, startingPoint.Right.Value }, SmallCaveVisitedTwice = false });
			}

			var startingRight = paths.Where(x => x.Right.Value.Equals(0));
			foreach (var startingPoint in startingRight)
			{
				part1PathsToTravel.Push(new List<int>() { startingPoint.Right.Value, startingPoint.Left.Value });
				part2PathsToTravel.Push(new Day12Part2Paths() { Path = new List<int>() { startingPoint.Right.Value, startingPoint.Left.Value }, SmallCaveVisitedTwice = false });
			}

			//Part 1
			while (part1PathsToTravel.Count > 0)
			{
				var thisPath = part1PathsToTravel.Pop();
				var lastVisitedElement = thisPath.Last();

				var leftBranches = paths.Where(x => x.Left.Value.Equals(lastVisitedElement));
				foreach (var branch in leftBranches)
				{
					if (branch.Right.Value.Equals(0))
						continue;
					else if (branch.Right.Value.Equals(-1))
					{
						var newSolution = new List<int>(thisPath);
						newSolution.Add(-1);
						part1PathsTraveled.Add(newSolution);
					}
					else
					{
						if (branch.Right.IsSmall)
						{
							if (!thisPath.Contains(branch.Right.Value))
							{
								var newSolution = new List<int>(thisPath);
								newSolution.Add(branch.Right.Value);
								part1PathsToTravel.Push(newSolution);
							}
						}
						else
						{
							var newSolution = new List<int>(thisPath);
							newSolution.Add(branch.Right.Value);
							part1PathsToTravel.Push(newSolution);
						}
					}
				}

				var rightBranches = paths.Where(x => x.Right.Value.Equals(lastVisitedElement));
				foreach (var branch in rightBranches)
				{
					if (branch.Left.Value.Equals(0))
						continue;
					else if (branch.Left.Value.Equals(-1))
					{
						var newSolution = new List<int>(thisPath);
						newSolution.Add(-1);
						part1PathsTraveled.Add(newSolution);
					}
					else
					{
						if (branch.Left.IsSmall)
						{
							if (!thisPath.Contains(branch.Left.Value))
							{
								var newSolution = new List<int>(thisPath);
								newSolution.Add(branch.Left.Value);
								part1PathsToTravel.Push(newSolution);
							}
						}
						else
						{
							var newSolution = new List<int>(thisPath);
							newSolution.Add(branch.Left.Value);
							part1PathsToTravel.Push(newSolution);
						}
					}
				}

			}

			day12Part1Solution = part1PathsTraveled.Count;

			//Part 2
			while (part2PathsToTravel.Count > 0)
			{
				var thisPath = part2PathsToTravel.Pop();
				var lastVisitedElement = thisPath.Path.Last();

				var leftBranches = paths.Where(x => x.Left.Value.Equals(lastVisitedElement));
				foreach (var branch in leftBranches)
				{
					if (branch.Right.Value.Equals(0))
						continue;
					else if (branch.Right.Value.Equals(-1))
					{
						var newSolution = new List<int>(thisPath.Path);
						newSolution.Add(-1);
						part2PathsTraveled.Add(newSolution);
					}
					else
					{
						if (branch.Right.IsSmall)
						{
							if (!thisPath.SmallCaveVisitedTwice)
							{
								if (thisPath.Path.Count(x => x.Equals(branch.Right.Value)) == 0)
								{
									var newSolution = new List<int>(thisPath.Path);
									newSolution.Add(branch.Right.Value);
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = newSolution, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
								}
								else if (thisPath.Path.Count(x => x.Equals(branch.Right.Value)) == 1)
								{
									var newSolution = new List<int>(thisPath.Path);
									newSolution.Add(branch.Right.Value);
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = newSolution, SmallCaveVisitedTwice = true });
								}
							}
							else
							{
								if (!thisPath.Path.Contains(branch.Right.Value))
								{
									var newSolution = new List<int>(thisPath.Path);
									newSolution.Add(branch.Right.Value);
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = newSolution, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
								}
							}
						}
						else
						{
							var newSolution = new List<int>(thisPath.Path);
							newSolution.Add(branch.Right.Value);
							part2PathsToTravel.Push(new Day12Part2Paths() { Path = newSolution, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
						}
					}
				}

				var rightBranches = paths.Where(x => x.Right.Value.Equals(lastVisitedElement));
				foreach (var branch in rightBranches)
				{
					if (branch.Left.Value.Equals(0))
						continue;
					else if (branch.Left.Value.Equals(-1))
					{
						var newSolution = new List<int>(thisPath.Path);
						newSolution.Add(-1);
						part2PathsTraveled.Add(newSolution);
					}
					else
					{
						if (branch.Left.IsSmall)
						{
							if (!thisPath.SmallCaveVisitedTwice)
							{
								if (thisPath.Path.Count(x => x.Equals(branch.Left.Value)) == 0)
								{
									var newSolution = new List<int>(thisPath.Path);
									newSolution.Add(branch.Left.Value);
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = newSolution, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
								}
								else if (thisPath.Path.Count(x => x.Equals(branch.Left.Value)) == 1)
								{
									var newSolution = new List<int>(thisPath.Path);
									newSolution.Add(branch.Left.Value);
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = newSolution, SmallCaveVisitedTwice = true });
								}
							}
							else
							{
								if (!thisPath.Path.Contains(branch.Left.Value))
								{
									var newSolution = new List<int>(thisPath.Path);
									newSolution.Add(branch.Left.Value);
									part2PathsToTravel.Push(new Day12Part2Paths() { Path = newSolution, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
								}
							}
						}
						else
						{
							var newSolution = new List<int>(thisPath.Path);
							newSolution.Add(branch.Left.Value);
							part2PathsToTravel.Push(new Day12Part2Paths() { Path = newSolution, SmallCaveVisitedTwice = thisPath.SmallCaveVisitedTwice });
						}
					}
				}

			}

			day12Part2Solution = part2PathsTraveled.Count;

			sw.Stop();
			Console.WriteLine($"Day 12, Part 1: {day12Part1Solution}");
			Console.WriteLine($"Day 12, Part 2: {day12Part2Solution}");
			Console.WriteLine($"Time Taken: {sw.ElapsedTicks}");
		}
	}
}