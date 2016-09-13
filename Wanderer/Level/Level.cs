using System;
using System.Collections.Generic;

namespace Level
{
	public class Level
	{
		#region WinAPI

		static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
		static readonly IntPtr HWND_TOP = new IntPtr(0);
		const UInt32 SWP_NOSIZE = 0x0001;
		const UInt32 SWP_NOMOVE = 0x0002;
		const UInt32 SWP_NOZORDER = 0x0004;
		const UInt32 SWP_NOREDRAW = 0x0008;
		const UInt32 SWP_NOACTIVATE = 0x0010;
		const UInt32 SWP_FRAMECHANGED = 0x0020;
		const UInt32 SWP_SHOWWINDOW = 0x0040;
		const UInt32 SWP_HIDEWINDOW = 0x0080;
		const UInt32 SWP_NOCOPYBITS = 0x0100;
		const UInt32 SWP_NOOWNERZORDER = 0x0200;
		const UInt32 SWP_NOSENDCHANGING = 0x0400;
		
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		 static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);		

		 #endregion
		
		[STAThread]		
		public static void Main(string[] args)
		{
			IntPtr ConsoleHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
			const UInt32 WINDOW_FLAGS = SWP_SHOWWINDOW;
			SetWindowPos(ConsoleHandle, HWND_NOTOPMOST, 2000, 100, 700, 350, WINDOW_FLAGS);

 			Console.SetBufferSize(80, 25);
 			var perlin2d = new Perlin2d( 23424234 );

 			List<Point> pointList = new List<Point>();
 			
 			for (int x = 0; x < 80; x++) 
 			{
 				pointList.Add( new Point( x, (int)perlin2d.Noise(1f,80f) ) );
 				
 			}
 			
			foreach (var point in pointList) 
			{
				point.Draw();
			}
			
 			Console.ReadLine();
		}
	}
	
	class Point
	{
		public int x;
		public int y;
		
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;			
		}
		
		public void Draw()
		{
			Console.SetCursorPosition(x,y);
			Console.Write('*');
 		}
	}
		
	public sealed class Perlin2d
	{
		private byte[] permutationTable;
		
	    public Perlin2d(int seed = 0)
	    {
	        var rand = new System.Random(seed);
	        permutationTable = new byte[1024];
	        rand.NextBytes(permutationTable); // заполняем случайными байтами
	    }		
		
    	public float Noise(float fx, float fy)
		{
	        // сразу находим координаты левой верхней вершины квадрата
	        int left = (int)System.Math.Floor(fx);
	        int top  = (int)System.Math.Floor(fy);
	
	        // а теперь локальные координаты точки внутри квадрата
	        float pointInQuadX = fx - left;
	        float pointInQuadY = fy - top;
	
	        // извлекаем градиентные векторы для всех вершин квадрата:
	        float[] topLeftGradient     = GetPseudoRandomGradientVector(left,   top  );
	        float[] topRightGradient    = GetPseudoRandomGradientVector(left+1, top  );
	        float[] bottomLeftGradient  = GetPseudoRandomGradientVector(left,   top+1);
	        float[] bottomRightGradient = GetPseudoRandomGradientVector(left+1, top+1);
	
	        // вектора от вершин квадрата до точки внутри квадрата:
	        float[] distanceToTopLeft     = new float[]{ pointInQuadX,   pointInQuadY   };
	        float[] distanceToTopRight    = new float[]{ pointInQuadX-1, pointInQuadY   };
	        float[] distanceToBottomLeft  = new float[]{ pointInQuadX,   pointInQuadY-1 };
	        float[] distanceToBottomRight = new float[]{ pointInQuadX-1, pointInQuadY-1 };
	
	        // считаем скалярные произведения между которыми будем интерполировать
			/*
			 tx1--tx2
			  |    |
			 bx1--bx2
			*/
	        float tx1 = Dot(distanceToTopLeft,     topLeftGradient);
	        float tx2 = Dot(distanceToTopRight,    topRightGradient);
	        float bx1 = Dot(distanceToBottomLeft,  bottomLeftGradient);
	        float bx2 = Dot(distanceToBottomRight, bottomRightGradient);
	
	        // готовим параметры интерполяции, чтобы она не была линейной:
	        pointInQuadX = QunticCurve(pointInQuadX);
	        pointInQuadY = QunticCurve(pointInQuadY);
	
	        // собственно, интерполяция:
	        float tx = Lerp(tx1, tx2, pointInQuadX);
	        float bx = Lerp(bx1, bx2, pointInQuadX);
	        float tb = Lerp(tx, bx, pointInQuadY);
	
	        // возвращаем результат:
	        return tb;
		}
    	
	    public float Noise(float fx, float fy, int octaves, float persistence = 0.5f)
	    {
	        float amplitude = 1; // сила применения шума к общей картине, будет уменьшаться с "мельчанием" шума
	        // как сильно уменьшаться - регулирует persistence
	        float max = 0; // необходимо для нормализации результата
	        float result = 0; // накопитель результата
	
	        while (octaves-- > 0)
	        {
	            max += amplitude;
	            result += Noise(fx, fy) * amplitude;
	            amplitude *= persistence;
	            fx *= 2; // удваиваем частоту шума (делаем его более мелким) с каждой октавой
	            fy *= 2;
	        }
	
	        return result/max;
	    }    	
		
		private static float Lerp(float a, float b, float t)
		{
			// return a * (t - 1) + b * t; можно переписать с одним умножением (раскрыть скобки, взять в другие скобки):
			return a + (b - a) * t;
		}	

		private static float QunticCurve(float t)
		{
			return t * t * t * (t * (t * 6 - 15) + 10);
		}
		
		private static float Dot(float[] a, float[] b)
		{
			return a[0] * b[0] + a[1] * b[1];
		}

	    private float[] GetPseudoRandomGradientVector(int x, int y)
	    {	    	
	        int v = (int)(((x * 1836311903) ^ (y * 2971215073) + 4807526976) & 1023);
	        v = permutationTable[v]&3;
	
	        switch (v)
	        {
        		case 0:  
	        		return new float[]{  1, 0 };
	            case 1:  
	        		return new float[]{ -1, 0 };
	            case 2:  
	        		return new float[]{  0, 1 };
	            default: 
	        		return new float[]{  0,-1 };
	        }
	    }		
	}	
}
