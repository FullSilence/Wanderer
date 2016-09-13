// disable ConvertToAutoProperty
using System;
using System.Collections.Generic;
using System.IO;

namespace Wanderer
{
	/// <summary>
	/// Абстрактный класс персонажей.
	/// </summary>
	public abstract class Character
	{		
		List<Action> actionList = new List<Action>();
		public List<Point> pointList;

		#region Поля

		private int x;
		private int y;		
		private char symbol = '*';
		private float speed = 1f;
		
		#endregion
		
		#region Свойства
		
		public int X {
			get {
				return x;
			}
			set {
				x = value;
			}
		}
		
		public int Y {
			get {
				return y;
			}
			set {
				y = value;
			}
		}
		
		protected char Symbol 
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

		protected float Speed 
		{
			get 
			{
				return speed;
			}
			set 
			{
				speed = value;
			}
		}
		
		#endregion
		
		public virtual bool Move(Direction direction)
		{			
			int xNext = x;
			int yNext = y;
			
			// ToDo: Добавить Speed
			switch (direction)
			{
				case Direction.Left:
					xNext--;
					break;
				case Direction.Right:
					xNext++;
					break;
				case Direction.Up:
					yNext--;
					break;
				case Direction.Down:
					yNext++;
					break;
				case Direction.Stop:
					return true;
			}
			
			if ( Collision(xNext, yNext ) )
			{
				return false;				
			}
			
			Clear();

			this.x = xNext;
			this.y = yNext;
			
			Draw();
			
			return true;
		}

		/// <summary>
		/// Стирает курсор по текущим координатам
		/// </summary>
		private void Clear()
		{
			Console.SetCursorPosition(x, y); // Стираем на старом месте
			Console.Write( ' ' );
		}	
		
		/// <summary>
		/// Рисует 'Symbol' в координатах X,Y
		/// </summary>
		private void Draw()
		{
			Console.SetCursorPosition(x,y);
			Console.Write(symbol);
		}
		
		/// <summary>
		/// Проверка на столкновения.
		/// </summary>
		/// <param name="pointList">Список точек-препятствий</param>
		/// <returns>Произошло ли столкновение</returns>
		public bool Collision( int xNext, int yNext )
		{
			foreach (Point point in pointList) 
			{
				if ( point.X == xNext && point.Y == yNext )
				{
					return true;			
				}			
			}			
			return false;
		}		
	}	
	
	public class Hero : Character
	{
		public Hero( int x, int y, char symbol = '*')
		{
			base.X = x;
			base.Y = y;
			base.Symbol = symbol;
		}
	}
}
