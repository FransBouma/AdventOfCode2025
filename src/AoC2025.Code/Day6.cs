using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SD.Tools.Algorithmia.Sorting;

namespace AoC2025.Core
{
	public static class Day6
	{
		public static List<List<int>> ConvertToDataList(List<string> input)
		{
			var toReturn = new List<List<int>>();
			for(int i=0;i<input.Count-1;i++)
			{
				var fragments = input[i].Split(' ');

				toReturn.Add(fragments.Where(f=>f.Trim().Length>0).Select(f=>int.Parse(f)).ToList());
			}
			// process last line. * is 1, + is 2
			var opFragments = input.Last().Split(' ');
			toReturn.Add(opFragments.Where(f=>f.Trim().Length>0).Select(f=>f=="*" ? 1 : 2).ToList());
			return toReturn;
		}


		public static List<string> PivotStrings(List<string> input)
		{
			// we skip the operators line
			var toReturn = new List<string>();
			var sb = new StringBuilder(input.Count-1);
			for(int column=input[0].Length-1;column>=0;column--)
			{ 
				sb.Clear();
				for(int row=0;row<input.Count-1;row++)
				{
					sb.Append(input[row][column]);
				}
				toReturn.Add(sb.ToString());
			}
			return toReturn;
		}


		public static long Solve1(List<string> input)
		{
			long toReturn = 0;
			var data = ConvertToDataList(input);
			var operators = data.Last();
			for(int column=0;column<data[0].Count;column++)
			{
				var op = operators[column];
				long value = op == 1 ? 1 : 0;
				for(int row=0;row<data.Count-1;row++)
				{
					value = op==1 ? value * data[row][column] : value + data[row][column];
				}
				Console.WriteLine($"{column}: {value}");
				toReturn += value;
			}
			return toReturn;
		}


		public static long Solve2(List<string> input)
		{
			long toReturn = 0;
			var operators = input.Last().Split(' ').Reverse().Where(f=>f.Trim().Length>0).ToList();

			var data = PivotStrings(input);

			int opIndex = 0;

			long value = operators[opIndex] == "*" ? 1 : 0;
			foreach(var currentLine in data)
			{
				if(string.IsNullOrWhiteSpace(currentLine))
				{
					toReturn+=value;
					opIndex++;
					value = operators[opIndex] == "*" ? 1 : 0;
				}
				else
				{
					var lineValue = long.Parse(currentLine);
					value = operators[opIndex] == "*" ? value *= lineValue : value+=lineValue;
				}
			}
			toReturn+=value;

			return toReturn;
		}
	}
}