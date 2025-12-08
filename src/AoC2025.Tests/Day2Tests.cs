using System;
using System.IO;
using System.Linq;
using AoC2025.Core;
using NUnit.Framework;

namespace AoC2025.Tests
{
	[TestFixture]
	public class Day2Tests
	{
		[OneTimeSetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day2_example.txt");
			Assert.That(input.Count>0);
			Assert.That(Day2.Solve1(input.First()), Is.EqualTo(1227775554));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day2.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day2.Solve1(input.First()));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day2_example2.txt");
			Assert.That(input.Count>0);
			Assert.That(Day2.Solve2(input.First()), Is.EqualTo(4174379265));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day2.txt");
			Assert.That(input.Count>0);
			Console.WriteLine(Day2.Solve2(input.First()));
		}
	}
}