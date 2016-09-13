using System;
using System.Collections.Generic;

namespace Wanderer
{
	/// <summary>
	/// Description of CharacterController.
	/// </summary>
	public class CharacterController
	{
		private readonly Character character = new Hero(20, 20);
		
		public CharacterController(List<Point> pointList)
		{
			character.pointList = pointList;
		}
		
		public void Move()
		{
			switch (Console.ReadKey().Key) 
			{
				case ConsoleKey.LeftArrow:
					character.Move(Direction.Left);
					break;
				case ConsoleKey.RightArrow:
					character.Move(Direction.Right);
					break;
				case ConsoleKey.UpArrow:
					character.Move(Direction.Up);
					break;
				case ConsoleKey.DownArrow:
					character.Move(Direction.Down);
					break;
				case ConsoleKey.Escape:
					Environment.Exit(0);
					break;
				default:
					character.Move(Direction.Stop);
					break;
			}
		}
	}
}
