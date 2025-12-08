using System;
using System.IO;
using System.Linq;
using AoC2025.Core;
using NUnit.Framework;

namespace AoC2025.Tests
{
	[TestFixture]
	public class Day3Tests
	{
		[OneTimeSetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day3_example.txt");
			Assert.That(input.Count>0);
			Assert.That(Day3.Solve1(input), Is.EqualTo(357));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day3.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day3.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day3_example2.txt");
			Assert.That(input.Count>0);
			Assert.That(Day3.Solve2(input), Is.EqualTo(3121910778619));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day3.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day3.Solve2(input));
		}
	}
}