using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2025.Core
{
	public static class Day2
	{
		private static List<string> ConvertInput(string input)
		{
			return input.Split(',').ToList();
		}


		public static long Solve1(string input)
		{
			var ranges = ConvertInput(input);
			long toReturn = 0;
			foreach(var range in ranges)
			{
				var fragments = range.Split('-');
				var currentId = long.Parse(fragments[0]);
				var endId = long.Parse(fragments[1]);
				while(currentId<=endId)
				{
					var currentAsString = currentId.ToString();
					var currentStringLength = currentAsString.Length;
					if(currentStringLength%2 ==0)
					{
						// even length, could be invalid...
						if(currentAsString.Substring(0, currentStringLength/2) == currentAsString.Substring(currentStringLength/2))
						{
							// invalid
							toReturn+=currentId;
							Console.WriteLine($"In range {range} the id {currentId} is invalid");
						}
					}
					currentId++;
				}
			}

			return toReturn;
		}


		public static long Solve2(string input)
		{
			var ranges = ConvertInput(input);
			long toReturn = 0;
			foreach(var range in ranges)
			{
				var fragments = range.Split('-');
				var currentId = long.Parse(fragments[0]);
				var endId = long.Parse(fragments[1]);
				while(currentId<=endId)
				{
					var currentAsString = currentId.ToString();
					var currentStringLength = currentAsString.Length;
					// grab first fragment till we reach a fragment size of 1
					int fragmentSizeDivider = 2;
					while(fragmentSizeDivider<=currentStringLength)
					{
						if(currentStringLength % fragmentSizeDivider==0)
						{
							var fragment = currentAsString.Substring(0, currentStringLength/fragmentSizeDivider);
							var toCompare = string.Empty;
							for(int i=0;i<fragmentSizeDivider;i++)
							{
								toCompare+=fragment;
							}
							if(currentAsString==toCompare)
							{
								// invalid
								toReturn+=currentId;
								Console.WriteLine($"In range {range} the id {currentId} is invalid");
								// already marked invalid, so we can stop with this id
								break;
							}
						}
						fragmentSizeDivider++;
					}
					currentId++;
				}
			}

			return toReturn;
		}
	}
}