using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2025.Core
{
	public static class Day7
	{
		public static List<int> FindSplitterOffsets(string row)
		{
			var toReturn= new List<int>();
			for(int i=0;i<row.Length;i++)
			{
				if(row[i]=='^')
				{
					toReturn.Add(i);
				}
			}
			return toReturn;
		}

		public static long Solve1(List<string> input)
		{
			long toReturn = 0;

			// we keep a linear array with true if we have a beam at that slot, false otherwise. 
			var beams = new bool[input[0].Length];
			beams[input[0].IndexOf('S')] = true;
			for(int i=1;i<input.Count;i++)
			{
				var offsets = FindSplitterOffsets(input[i]);
				if(offsets.Count<=0)
				{
					continue;
				}
				foreach(var offset in offsets)
				{
					if(beams[offset])
					{
						beams[offset] = false;
						if(offset>0)
						{
							beams[offset-1] = true;
						}
						if(offset<beams.Length-1)
						{
							beams[offset+1] = true;
						}
						toReturn++;
					}
				}
			}
			return toReturn;
		}


		public static long Solve2(List<string> input)
		{
			long toReturn = 0;

			// we count the # of beams at a given slot. Puzzle 1 ignored duplicates on a slot, puzzle 2 keeps them, as they're valid paths
			// then simply count all beams that we end up with. In theory a directed graph with all end points as nodes and then DFS or BFS to find all paths is 
			// also correct but given the big input that will run forever so we need a different approach. 
			var beams = new long[input[0].Length];
 
			beams[input[0].IndexOf('S')] = 1;

			for(int i=1;i<input.Count;i++)
			{
				var offsets = FindSplitterOffsets(input[i]);
				if(offsets.Count<=0)
				{
					continue;
				}
				foreach(var offset in offsets)
				{
					long amountOfBeamsOnSlot = beams[offset];
					if(amountOfBeamsOnSlot> 0)
					{
						if(offset>0)
						{
							beams[offset-1] += amountOfBeamsOnSlot;
						}
						if(offset<beams.Length-1)
						{
							beams[offset+1] += amountOfBeamsOnSlot;
						}
						beams[offset] = 0;
					}
				}
			}
			toReturn = beams.Sum();
			return toReturn;
		}
	}
}