using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SD.Tools.Algorithmia.GeneralDataStructures;

namespace AoC2025.Core
{
	public static class Day10
	{
		public static long Solve1(List<string> input)
		{
			// Pressing a button twice is the same as pressing it not at all, so a button is only pressed once at the most
			// this means the total solution always has n (number of buttons) or less steps
			// buttons basically represent binary numbers, and the solution is the xor-red result of all buttons pressed (with the exception that 0 xor 0 still is 0)
			// to find the solution, check which light has to be on, and start with a button that has that light bit set
			// then use a hashset to weed out duplicate button presses and select the buttons to press to get the lights that still
			// need to be toggled which are the buttons which have that light's bit set so it's toggled. 
			return 0;
		}


		public static long Solve2(List<string> input)
		{
			return 0;
		}
	}
}