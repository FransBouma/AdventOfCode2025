using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.Sorting;

namespace AoC2025.Core
{
	public class Range
	{
		public long Start {get;set;}
		public long End {get;set;}
		public long Length => (End-Start) + 1;

		public bool IsInRange(long value)
		{
			return value >= Start && value <= End;
		}

		public int Compare(Range toCompareWith)
		{
			return Start<toCompareWith.Start ? -1 : Start > toCompareWith.Start ? 1 : 0;
		}

		public bool HasOverlapWith(Range toMerge)
		{
			return IsInRange(toMerge.Start) || IsInRange(toMerge.End) || toMerge.IsInRange(Start) || toMerge.IsInRange(End);
		}

		public void Merge(Range toMergeIntoThis)
		{
			if(!HasOverlapWith(toMergeIntoThis))
			{
				return;
			}
			if(!IsInRange(toMergeIntoThis.Start))
			{
				this.Start = toMergeIntoThis.Start;
			}
			if(!IsInRange(toMergeIntoThis.End))
			{
				this.End = toMergeIntoThis.End;
			}
		}
	}

	public static class Day5
	{
		public static long Solve1(List<string> input)
		{
			long toReturn = 0;
			var ranges = new List<Range>();
			int index = 0;
			while(!string.IsNullOrWhiteSpace(input[index]))
			{
				var fragments = input[index].Split('-');
				ranges.Add(new Range() { Start = long.Parse(fragments[0]), End = long.Parse(fragments[1])});
				index++;
			}

			index++;

			while(index<input.Count)
			{
				var value = long.Parse(input[index]);
				foreach(var r in ranges)
				{
					if(r.IsInRange(value))
					{
						toReturn++;
						break;
					}
				}
				index++;
			}

			return toReturn;
		}

		public static long Solve2(List<string> input)
		{
			long toReturn = 0;

			var ranges = new List<Range>();
			int index = 0;
			while(!string.IsNullOrWhiteSpace(input[index]))
			{
				var fragments = input[index].Split('-');
				ranges.Add(new Range() { Start = long.Parse(fragments[0]), End = long.Parse(fragments[1])});
				index++;
			}
			ranges.Sort(SD.Tools.Algorithmia.SortAlgorithm.ShellSort, SD.Tools.Algorithmia.SortDirection.Ascending, (a,b)=>a.Compare(b));

			var mergedRanges = new List<Range>();
			Range mergedRange = null;
			for(int i=0;i<ranges.Count;i++)
			{
				var currentRange = ranges[i];
				if(mergedRange==null)
				{
					mergedRange = currentRange;
					continue;
				}
				if(!currentRange.HasOverlapWith(mergedRange))
				{
					mergedRanges.Add(mergedRange);
					mergedRange = currentRange;
					continue;
				}
				mergedRange.Merge(currentRange);
			}
			if(mergedRange!=null)
			{
				mergedRanges.Add(mergedRange);
			}

			foreach(var mr in mergedRanges)
			{
				toReturn+=mr.Length;
			}

			return toReturn;
		}
	}
}