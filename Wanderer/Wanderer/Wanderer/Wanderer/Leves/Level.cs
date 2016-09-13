using System.Collections.Generic;
namespace Wanderer.Leves
{
	public class Level
	{
		public List<Point> walls = new List<Point>();
		
		public Level()
		{
			for (int x = 0; x < 79; x++) 
			{
				walls.Add( new Point(x, 0,'+') );
				walls.Add( new Point(x, 24,'+'));
			}			
			
			for (int y = 0; y < 24; y++) 
			{
				walls.Add( new Point(0, y,'+') );
				walls.Add( new Point(78, y,'+'));				
			}
		}
		
		public void Draw()
		{
			foreach (Point point in walls) 
			{
				point.Draw();
			}
		}
	}
}
