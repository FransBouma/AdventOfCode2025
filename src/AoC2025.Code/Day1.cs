using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2025.Core
{
	public static class Day1
	{
		public static int Solve1(List<string> input)
		{
			int value = 50;
			int numberOfTimesAt0 = 0;
			foreach(var v in input)
			{
				if(string.IsNullOrEmpty(v))
				{
					break;
				}
				var cmd = v[0];
				var ticks =  int.Parse(v.Substring(1));
				value = cmd == 'L' ? value - ticks : value + ticks;
				value = (value + 100)  % 100;
				if(value == 0)
				{
					numberOfTimesAt0++;
				}
			}
			
			return numberOfTimesAt0;
		}


		public static int Solve2(List<string> input)
		{
			int value = 50;
			int numberOfTimesAt0 = 0;
			foreach(var v in input)
			{
				if(string.IsNullOrEmpty(v))
				{
					break;
				}
				
				var cmd = v[0];
				var ticks =  int.Parse(v.Substring(1));
				var previousValue = value;
				value = cmd == 'L' ? value - ticks : value + ticks;
				// value can be negative, in which case we run into problems later, so we have to clamp again. 
				value = (((value + 100)  % 100) + 100) % 100;
				if(value == 0)
				{
					numberOfTimesAt0+=(ticks / 100) + 1;
				}
				else
				{
					// check if we went through 0. this happens when:
					// 1 - start at 0 and rotate > 100 steps left
					// 2 - start at 0 and rotate > 100 steps right
					// 3 - start at n, n!=0 and rotate > n steps left
					// 4 - start at n, n!=0 and rotate > (100-n) + 1 steps right
					// 
					// if so, how many times? 
					// 1: number of steps / 100
					// 2: number of steps / 100
					// 3: 1 + ((number of steps - n) / 100)
					// 4: 1 + (((number of steps - (100-n))	/ 100)			#steps: R43. n: 58. results in 101, passes 0
					if(previousValue == 0)
					{
						numberOfTimesAt0 += (ticks / 100);
					}
					else
					{
						if(cmd == 'L')
						{
							if(ticks > previousValue)
							{
								var toAdd = 1 +((ticks -  previousValue) / 100);
								numberOfTimesAt0 += toAdd;
							}
						}
						else
						{
							// 'R'
							if(ticks >= ((100-previousValue)+1))
							{
								var toAdd = 1 + ((ticks - (100-previousValue)) / 100);
								numberOfTimesAt0 += toAdd;
							}
						}
					}
				}
			}
	
			return numberOfTimesAt0;
		}
	}
}