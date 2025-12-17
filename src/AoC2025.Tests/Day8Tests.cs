using System;
using System.IO;
using System.Linq;
using AoC2025.Core;
using NUnit.Framework;

namespace AoC2025.Tests
{
	[TestFixture]
	public class Day8Tests
	{
		[OneTimeSetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8_example.txt");
			Assert.That(input.Count>0);
			Assert.That(Day8.Solve1(input), Is.EqualTo(40));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day8.Solve1(input, 1000));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8_example2.txt");
			Assert.That(input.Count>0);
			Assert.That(Day8.Solve2(input, 10), Is.EqualTo(25272));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day8.Solve2(input, 1000));
		}
	}
}