using System;
using Wanderer.Leves;

namespace Wanderer
{
	class Game
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

			var level = new Level();
			level.Draw();
			
			var characterController = new CharacterController(level.walls);		
		
			while (true) 
			{
				characterController.Move();
			}
		}
	}
	
}