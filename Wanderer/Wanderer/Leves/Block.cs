using System;

namespace Wanderer.Leves
{
	/// <summary>
	/// Блок
	/// </summary>
	public class Block
	{
		#region Поля

		private int size;			// Размер блока
		private TypeBlock blockType;		// Тип блока
		private int integrality; 	// Целостность

		#endregion

		#region Свойства

		public int Size {
			get {
				return size;
			}
			set {
				size = value;
			}
		}

		public TypeBlock BlockType {
			get {
				return blockType;
			}
			set {
				blockType = value;
			}
		}

		public int Integrality {
			get {
				return integrality;
			}
			set {
				integrality = value;
			}
		}
		#endregion

		private Block()
		{
		}
		
		public Block(int size, TypeBlock type, int integrality)
		{
			if (size < 1 || size > 1000)
				throw new ArgumentOutOfRangeException("Размер блока", size, "Значение должно быть от " + 1 + " до " + 1000);
			if (integrality < 1 || integrality > 100)
				throw new ArgumentOutOfRangeException("Целостность", integrality, "Значение должно быть от " + 1 + " до " + 100);
			
			this.size = size;
			this.blockType = type;
			this.integrality = integrality;			
		}
	}
}
