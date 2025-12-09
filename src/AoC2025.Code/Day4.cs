using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2025.Core
{
	public static class Day4
	{
		private static bool IsInsideArray(int x, int y, char[,] a)
		{
			return (x>=0 && x<a.GetLength(1)) && (y>=0 && y < a.GetLength(0));
		}


		private static bool IsValidRoll(int x, int y, char[,] a)
		{
			if(a[x, y]!='@')
			{
				return false;
			}
			int noOfRolls = 0;
			noOfRolls += (IsInsideArray(x, y-1, a) && a[x, y-1]=='@') ? 1: 0;
			noOfRolls += (IsInsideArray(x+1, y-1, a) && a[x+1, y-1]=='@') ? 1: 0;
			noOfRolls += (IsInsideArray(x+1, y, a) && a[x+1, y]=='@') ? 1: 0;
			noOfRolls += (IsInsideArray(x+1, y+1, a) && a[x+1, y+1]=='@') ? 1: 0;
			noOfRolls += (IsInsideArray(x, y+1, a) && a[x, y+1]=='@') ? 1: 0;
			noOfRolls += (IsInsideArray(x-1, y+1, a) && a[x-1, y+1]=='@') ? 1: 0;
			noOfRolls += (IsInsideArray(x-1, y, a) && a[x-1, y]=='@') ? 1: 0;
			noOfRolls += (IsInsideArray(x-1, y-1, a) && a[x-1, y-1]=='@') ? 1: 0;
			return(noOfRolls<4);
		}

		public static long Solve1(char[,] input)
		{
			long toReturn = 0;

			for(int y=0;y<input.GetLength(0);y++)
			{
				for(int x=0;x<input.GetLength(1);x++)
				{
					toReturn+=IsValidRoll(x, y, input) ? 1 : 0;
				}
			}

			return toReturn;
		}


		public static long Solve2(char[,] input)
		{
			long toReturn = 0;

			var currentInput = input.Clone() as char[,];
			var previousInput = input.Clone() as char[,];

			int numberOfRollsRemovedInPass = int.MaxValue;
			while(numberOfRollsRemovedInPass>0)
			{
				previousInput = currentInput.Clone() as char[,];
				numberOfRollsRemovedInPass=0;
				for(int y=0;y<previousInput.GetLength(0);y++)
				{
					for(int x=0;x<previousInput.GetLength(1);x++)
					{
						if(IsValidRoll(x, y, previousInput))
						{
							numberOfRollsRemovedInPass++;
							currentInput[x, y] = '.';
						}
						else
						{
							currentInput[x, y] = previousInput[x, y];
						}
					}
				}
				toReturn+=numberOfRollsRemovedInPass;
			}
			return toReturn;
		}
	}
}