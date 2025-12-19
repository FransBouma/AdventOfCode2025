using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2025.Core
{
	public static class Day9
	{
		static long _minX = long.MaxValue;
		static long _minY = long.MaxValue;
		static long _width = 0;
		static long _height= 0;

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


		private static long GetX(long x)
		{
			return x-_minX;
		}


		private static long GetY(long y)
		{
			return y-_minY;
		}


		public static long Solve2(List<string> input)
		{
			// this is basically the algorithm the Amiga blitter used for filling a polygon
			var points = input.Select(s=>s.Split(',')).Select(a=>new Point2D(){X = long.Parse(a[0]), Y = long.Parse(a[1])}).ToList();
			// determine 2D array size
			long maxX = 0;
			long maxY = 0;
			foreach(var p in points)
			{
				maxX = Math.Max(maxX, p.X);
				maxY = Math.Max(maxY, p.Y);
				_minX = Math.Min(_minX, p.X);
				_minY = Math.Min(_minY, p.Y);
			}

			Console.WriteLine("Initializing array");

			_width = maxX - _minX;
			_height = maxY - _minY;

			var field = new char[_height+1][];
			for(int y = 0;y<=_height;y++)
			{
				field[y] = new char[_width+1];
				for(int x = 0;x<=_width;x++)
				{
					field[y][x] = '.';
				}
			}

			Console.WriteLine("Array initialized");

			// Draw vertices and lines
			for(int i=0;i<points.Count;i++)
			{
				var pointA = points[i];
				var pointB = i==points.Count-1 ? points[0] : points[i+1];
				field[GetY(pointA.Y)][GetX(pointA.X)] = '#';
				// draw line between A and B with X
				if(pointA.X == pointB.X)
				{
					// vertical
					var start = Math.Min(pointA.Y, pointB.Y);
					var end = Math.Max(pointA.Y, pointB.Y);
					for(long y = start;y<end;y++)
					{
						if(field[GetY(y)][GetX(pointA.X)]!='.')
						{
							continue;
						}
						field[GetY(y)][GetX(pointA.X)] = 'X';
					}
				}
				// Horizontal lines are done with the flood fill
			}

			Console.WriteLine("Vertical lines drawn");

			//for(int y = 0;y<=_height;y++)
			//{
			//	for(int x = 0;x<=_width;x++)
			//	{
			//		Console.Write(field[y][x]);
			//	}
			//	Console.WriteLine();
			//}

			// then use a floodfill algorithm to fill the areas, this is a simple top down algorithm which sweeps left to right and keeps track of whether the beam is inside the area.
			for(int y = 0;y<=_height;y++)
			{
				int x = 0;
				bool inArea = false;
				while(x<=_width)
				{
					switch(field[y][x])
					{
						case '.':
							if(inArea)
							{
								field[y][x] = 'X';
							}
							break;
						case '#':
						case 'X':
							int xOfNextOnLine = FindNext(field, x, y);
							inArea = xOfNextOnLine>=0;
							break;
					}
					x++;
				}
			}

			Console.WriteLine("Figure filled");

			//for(int y = 0;y<=_height;y++)
			//{
			//	for(int x = 0;x<=_width;x++)
			//	{
			//		Console.Write(field[y][x]);
			//	}
			//	Console.WriteLine();
			//}

			// then for each pair of corners, determine if between them is a '.' char. If so, they're not forming a valid rectangle. If they do, calculate the Rectangle size. 
			long maxSize = 0;
			for(int i=0;i<points.Count;i++)
			{
				for(int j=i+1;j<points.Count;j++)
				{
					// check if in the area formed by the two points is any '.'. if so, it's an invalid rectangle. 
					var pointA = points[i];
					var pointB = points[j];

					var aOpposite = new Point2D(){X = pointA.X, Y=pointB.Y };
					var bOpposite = new Point2D(){X = pointB.X, Y=pointA.Y };

					bool isValid = true;
					for(long y=Math.Min(pointA.Y, pointB.Y);y<=Math.Max(pointA.Y, pointB.Y);y++)
					{
						for(long x = Math.Min(pointA.X, pointB.X);x<=Math.Max(pointA.X, pointB.X);x++)
						{
							if(field[GetY(y)][GetX(x)]=='.')
							{
								isValid = false;
								break;
							}
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

		public static int FindNext(char[][] field, int x, int y)
		{
			int toReturn = -1;
			int currentX = x+1;
			while(currentX<=_width)
			{
				if(field[y][currentX]=='.')
				{
					currentX++;
				}
				else
				{
					// found it
					toReturn = currentX;
					break;
				}
			}

			return toReturn;
		}
	}
}