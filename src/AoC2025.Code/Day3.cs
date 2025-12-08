using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2025.Core
{
	public static class Day3
	{
		private static (int digit, int pos) GetHighestDigit(string s, int offsetFromEndToStop = 1)
		{
			int currentHighestDigit = -1;
			int currentHighestDigitPos = -1;

			for(int i=0;i<s.Length;i++)
			{
				int digit = s[i]-'0';
				if(currentHighestDigit<digit)
				{
					currentHighestDigit = digit;
					currentHighestDigitPos = i;
				}
				if(digit==9 || (i>=s.Length-(1+offsetFromEndToStop)))
				{
					break;
				}
			}

			return (currentHighestDigit, currentHighestDigitPos);
		}

		public static long Solve1(List<string> input)
		{
			long toReturn = 0;
			foreach(var s in input)
			{
				(var firstDigit, var firstDigitPos) = GetHighestDigit(s);
				(var secondDigit, var secondDigitPos) = GetHighestDigit(s.Substring(firstDigitPos+1), offsetFromEndToStop:0);
				var toAdd = firstDigit*10 + secondDigit;
				toReturn+= toAdd;
			}

			return toReturn;
		}


		public static long Solve2(List<string> input)
		{
			long toReturn = 0;
			foreach(var s in input)
			{
				var foundValue = new char[12];
				var startpos = 0;
				for(int i=0;i<12;i++)
				{
					var sizeLeft = 12-(i+1);
					(var digit, var pos) = GetHighestDigit(s.Substring(startpos), sizeLeft);
					foundValue[i] = digit.ToString()[0];
					startpos += pos+1;
				}
				var toAdd = long.Parse(new string(foundValue));
				toReturn+= toAdd;
			}

			return toReturn;
		}
	}
}