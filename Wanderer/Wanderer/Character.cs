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
		private int x;
		private int y;
		
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
		
		List<Action> actionList = new List<Action>();		
		
		public virtual bool Move( Direction direction )
		{
			return true;
		}
		
	}	
}
