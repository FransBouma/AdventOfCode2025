using System;
using System.IO;
using AoC2025.Core;
using NUnit.Framework;

namespace AoC2025.Tests
{
	[TestFixture]
	public class Day1Tests
	{
		[OneTimeSetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day1_example.txt");
			Assert.That(input.Count>0);
			Assert.That(Day1.Solve1(input), Is.EqualTo(3));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day1.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day1.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day1_example2.txt");
			Assert.That(input.Count>0);
			Assert.That(Day1.Solve2(input), Is.EqualTo(16));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day1.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day1.Solve2(input));
		}
	}
}