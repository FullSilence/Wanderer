using System;
namespace Wanderer
{
	public class Point
	{
		private int x;
		private int y;		
		private char symbol;

		public int X 
		{
			get 
			{
				return x;
			}
			set 
			{
				x = value;
			}
		}

		public int Y 
		{
			get 
			{
				return y;
			}
			set 
			{
				y = value;
			}
		}

		public char Symbol 
		{
			get 
			{
				return symbol;
			}
			set 
			{
				symbol = value;
			}
		}
		
		public Point(int x, int y, char symbol = '*')
		{
			this.x = x;
			this.y = y;
			this.symbol = symbol;			
		}		
		
		public void Draw()
		{
			Console.SetCursorPosition(x,y);
			Console.Write(symbol);
 		}
	}

}
