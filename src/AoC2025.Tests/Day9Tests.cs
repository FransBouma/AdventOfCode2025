using System;
using System.IO;
using System.Linq;
using AoC2025.Core;
using NUnit.Framework;

namespace AoC2025.Tests
{
	[TestFixture]
	public class Day9Tests
	{
		[OneTimeSetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day9_example.txt");
			Assert.That(input.Count>0);
			Assert.That(Day9.Solve1(input), Is.EqualTo(50));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day9.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day9.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day9_example.txt");
			Assert.That(input.Count>0);
			Assert.That(Day9.Solve2(input), Is.EqualTo(24));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day9.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day9.Solve2(input));
		}
	}
}