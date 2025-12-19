using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2025.Core
{
	public static class Day8
	{
		public class Point3D
		{
			public long X;
			public long Y;
			public long Z;

			public double Distance(Point3D p)
			{
				return Math.Sqrt(((X-p.X) * (X-p.X)) + ((Y-p.Y) * (Y-p.Y)) + ((Z-p.Z) * (Z-p.Z)));
			}
		}


		public class Distance
		{
			public int PointAOffset;
			public int PointBOffset;

			public double AbsoluteDistance;
		}

		public static int FindCircuitOffset(List<HashSet<int>> circuits, int pointOffset)
		{
			for(int i=0;i<circuits.Count;i++)
			{
				if(circuits[i].Contains(pointOffset))
				{
					return i;
				}
			}
			return -1;
		}


		private static SortedList<double, Distance> BuildDataSet(List<string> input, int numberOfElements, List<Point3D> points, List<HashSet<int>> circuits)
		{
			foreach(var s in input)
			{
				var fragments = s.Split(',');
				points.Add(new Point3D() { X = long.Parse(fragments[0]), Y = long.Parse(fragments[1]), Z = long.Parse(fragments[2]) });
			}

			var sortedDistances = new SortedList<double, Distance>(numberOfElements);
			for(int i = 0; i < points.Count; i++)
			{
				for(int j = i + 1; j < points.Count; j++)
				{
					var distance = points[i].Distance(points[j]);
					// we reasonably limit the dataset as most distances are ignored.
					if(sortedDistances.Count < numberOfElements || (sortedDistances.GetValueAtIndex(numberOfElements - 1).AbsoluteDistance > distance))
					{
						sortedDistances.Add(distance, new Distance() { PointAOffset = i, PointBOffset = j, AbsoluteDistance = distance });
					}
				}
			}

			for(int i = 0; i < points.Count; i++)
			{
				// create the initial circuits which are all points in their own individual circuit. 
				circuits.Add(new HashSet<int>() { i });
			}

			return sortedDistances;
		}


		public static long Solve1(List<string> input, int numberOfElements=10)
		{
			var points = new List<Point3D>();
			var circuits = new List<HashSet<int>>();
			SortedList<double, Distance> sortedDistances = BuildDataSet(input, numberOfElements, points, circuits);

			for(int i = 0; i < numberOfElements; i++)
			{
				var d = sortedDistances.GetValueAtIndex(i);
				// find the circuit for point a and the circuit for point b and merge them into the circuit for point a, if they're not the same. 
				var circuitOffsetA = FindCircuitOffset(circuits, d.PointAOffset);
				var circuitOffsetB = FindCircuitOffset(circuits, d.PointBOffset);

				if(circuitOffsetA == circuitOffsetB)
				{
					// already in the same circuit
					continue;
				}
				var circuitA = circuits[circuitOffsetA];
				var circuitB = circuits[circuitOffsetB];
				circuits.RemoveAt(circuitOffsetB);
				circuitA.AddRange(circuitB);
			}
			var sizesToMultiply = circuits.Select(c => c.Count).OrderByDescending(c => c).Take(3).ToList();

			long toReturn = 1;
			for(int i = 0; i < sizesToMultiply.Count; i++)
			{
				toReturn *= sizesToMultiply[i];
			}

			return toReturn;
		}


		public static long Solve2(List<string> input, int numberOfElements=10)
		{
			var points = new List<Point3D>();
			var circuits = new List<HashSet<int>>();
			SortedList<double, Distance> sortedDistances = BuildDataSet(input, numberOfElements*10, points, circuits);

			int pointAOffset = -1;
			int pointBOffset = -1;
			for(int i = 0;i<sortedDistances.Count;i++)
			{
				var d = sortedDistances.GetValueAtIndex(i);
				// find the circuit for point a and the circuit for point b and merge them into the circuit for point a, if they're not the same. 
				var circuitOffsetA = FindCircuitOffset(circuits, d.PointAOffset);
				var circuitOffsetB = FindCircuitOffset(circuits, d.PointBOffset);

				if(circuitOffsetA == circuitOffsetB)
				{
					// already in the same circuit
					continue;
				}
				var circuitA = circuits[circuitOffsetA];
				var circuitB = circuits[circuitOffsetB];
				circuits.RemoveAt(circuitOffsetB);
				circuitA.AddRange(circuitB);
				if(circuits.Count==1)
				{
					pointAOffset = d.PointAOffset;
					pointBOffset = d.PointBOffset;
					break;
				}
			}
			return points[pointAOffset].X * points[pointBOffset].X;
		}
	}
}