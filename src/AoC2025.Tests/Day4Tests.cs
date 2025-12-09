using System;
using System.IO;
using System.Linq;
using AoC2025.Core;
using NUnit.Framework;

namespace AoC2025.Tests
{
	[TestFixture]
	public class Day4Tests
	{
		[OneTimeSetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAs2DCharArrayList("..\\..\\..\\PuzzleInputs\\day4_example.txt");
			Assert.That(input.Length>0);
			Assert.That(Day4.Solve1(input), Is.EqualTo(13));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAs2DCharArrayList("..\\..\\..\\PuzzleInputs\\day4.txt");
			Assert.That(input.Length>0);
			Console.WriteLine(Day4.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAs2DCharArrayList("..\\..\\..\\PuzzleInputs\\day4_example2.txt");
			Assert.That(input.Length>0);
			Assert.That(Day4.Solve2(input), Is.EqualTo(43));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAs2DCharArrayList("..\\..\\..\\PuzzleInputs\\day4.txt");
			Assert.That(input.Length>0);
			Console.WriteLine(Day4.Solve2(input));
		}
	}
}