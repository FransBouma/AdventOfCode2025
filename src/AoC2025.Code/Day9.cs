using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SD.Tools.Algorithmia.GeneralDataStructures;

namespace AoC2025.Core
{
	public static class Day9
	{
		public class Point2D
		{
			public long X;
			public long Y;

			public long RectangleSize(Point2D p)
			{
				return (Math.Abs(X-p.X)+1) * (Math.Abs(Y - p.Y)+1);
			}
		}


		public static long Solve1(List<string> input)
		{
			var points = input.Select(s=>s.Split(',')).Select(a=>new Point2D(){X = long.Parse(a[0]), Y = long.Parse(a[1])}).ToList();
			long maxSize = 0;
			for(int i=0;i<points.Count;i++)
			{
				for(int j=i+1;j<points.Count;j++)
				{
					var rectSize = points[i].RectangleSize(points[j]);
					maxSize = Math.Max(rectSize, maxSize);
				}
			}

			return maxSize;
		}


		public static long Solve2(List<string> input)
		{
			// this is basically the algorithm the Amiga blitter used for filling a polygon
			var points = input.Select(s=>s.Split(',')).Select(a=>new Point2D(){X = long.Parse(a[0]), Y = long.Parse(a[1])}).ToList();

			// however using a full array with points isn't going to work as it requires > 10GB of memory and even allocating that in separate arrays it's taking forever. 
			// so we will use a different approach for representing that array: we'll store per Y a small list of x values which describe from-to pairs of the pixels which are set
			// so say the start is .X...X..X....X.
			// then the list for this y coord will contain 2 pairs: 1,5 and 8,13. We call the from-to pairs 'spans'. 
			// We'll then merge the lists after we created them to concat and pairs that are connected. (e.g. 1,4 and 5,7 will become 1,7)
			// We can then use this to test if a rectangle is in the polygon by checking if any containing point in the rectangle is covering a point in one of these pairs. 

			long maxX = 0;
			long maxY = 0;
			foreach(var p in points)
			{
				maxX = Math.Max(maxX, p.X);
				maxY = Math.Max(maxY, p.Y);
			}

			var spansPerY = new Dictionary<long, List<Pair<long, long>>>((int)maxY+1);
			var pointsPerY = new Dictionary<long, HashSet<long>>((int)maxY+1);		// per Y we keep the x coord which contains a pixel on that horizontal line.
			for(int i=0;i<=maxY;i++)
			{
				spansPerY[i] = new List<Pair<long, long>>();
				pointsPerY[i] = new HashSet<long>();
			}

			// Draw vertices and lines. We do this by adding a pair for each vertical pixel we set to the spansPerY set. 
			for(int i=0;i<points.Count;i++)
			{
				var pointA = points[i];
				var pointB = i==points.Count-1 ? points[0] : points[i+1];

				var pointsOnY = pointsPerY[pointA.Y];
				pointsOnY.Add(pointA.X);

				// draw line between A and B
				if(pointA.X == pointB.X)
				{
					// vertical
					var start = Math.Min(pointA.Y, pointB.Y);
					var end = Math.Max(pointA.Y, pointB.Y);
					for(long y = start;y<end;y++)
					{
						pointsOnY = pointsPerY[y];
						pointsOnY.Add(pointA.X);
					}
				}
				// Horizontal lines are done with the flood fill
			}

			/*
			// debug render
			foreach(var kvp in pointsPerY.OrderBy(v=>v.Key))
			{
				var orderedPoints = kvp.Value.OrderBy(v=>v).ToList();
				var line = new char[maxX+1];
				for(int x = 0;x<=maxX;x++)
				{
					line[x] = '.';
				}
				foreach(var p in orderedPoints)
				{
					line[p] = 'X';
				}
				var s = new string(line);
				Console.WriteLine(s);
			}
			*/

			// then use a floodfill algorithm to fill the areas, this is a simple top down algorithm which sweeps left to right and keeps track of whether the beam is inside the area.
			// all entries in the span lists are single pixels, so we connect them together
			for(int y = 0;y<=maxY;y++)
			{
				var pointsOnY = pointsPerY[y].OrderBy(s=>s).ToList();
				int xIndex = 0;
				var spansOnY = spansPerY[y];
				while(xIndex < pointsOnY.Count)
				{
					// connect the two points next to each other in the list with each other. 
					// then proceed to the next till all spans are done. 
					long start = pointsOnY[xIndex];
					long end  =pointsOnY[xIndex+1];
					// special case: if there's 1 pixel left, we connect it to the last span. 
					if(xIndex==pointsOnY.Count-3)
					{
						end  = pointsOnY[xIndex+2];
						xIndex=pointsOnY.Count; // makes sure we'll exit the while loop
					}
					spansOnY.Add(new Pair<long, long>(start, end));
					xIndex+=2;
				}
			}

			/*
			// Debug render
			foreach(var kvp in spansPerY.OrderBy(v=>v.Key))
			{
				var orderedSpans = kvp.Value.OrderBy(v=>v.Value1).ToList();
				var line = new char[maxX+1];
				for(int x = 0;x<=maxX;x++)
				{
					line[x] = '.';
				}
				foreach(var pair in orderedSpans)
				{
					for(long x = pair.Value1;x<=pair.Value2;x++)
					{
						line[x] = 'X';
					}
				}
				var s = new string(line);
				Console.WriteLine(s);
			}
			*/

			// then for each line in the rectangle we're examining, we will look up the spans on the y coordinate of that line. if the span isn't covering the left/right x of the current
			// line of the rectangle, the rectangle goes outside the polygon and it's invalid. 
			long maxSize = 0;
			for(int i=0;i<points.Count;i++)
			{
				for(int j=i+1;j<points.Count;j++)
				{
					var pointA = points[i];
					var pointB = points[j];

					var aOpposite = new Point2D(){X = pointA.X, Y=pointB.Y };
					var bOpposite = new Point2D(){X = pointB.X, Y=pointA.Y };

					// so we go from minY to maxY in the rect, and then per line from minX to maxX. 

					bool isValid = true;
					var minXRect = Math.Min(pointA.X, pointB.X);
					var maxXRect = Math.Max(pointA.X, pointB.X);
					for(long y=Math.Min(pointA.Y, pointB.Y);y<=Math.Max(pointA.Y, pointB.Y);y++)
					{
						var spans = spansPerY[y];

						// check if there's a span in this span set which covers minxRect-maxxRect
						if(!spans.Any(s=>s.Value1<=minXRect && s.Value2>=maxXRect))
						{
							isValid=false;
							break;
						}
					}
					if(!isValid)
					{
						continue;
					}
					var rectSize = points[i].RectangleSize(points[j]);
					maxSize = Math.Max(rectSize, maxSize);
				}
			}
			return maxSize;
		}
	}
}