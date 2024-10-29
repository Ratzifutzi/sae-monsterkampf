using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf.HelperClasses
{
	internal class ConsoleHelper
	{
		/// <summary>
		/// Clears the line provided
		/// </summary>
		/// <param name="line">The line that should be cleared</param>
		public void ClearLine(int line)
		{
			Console.SetCursorPosition(0, line);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(0, line);
		}

		/// <summary>
		/// Sends an error message and sets the cursor position back to the input line. Used for input validation
		/// </summary>
		/// <param name="message">The error message that should be displayed</param>
		public void SendErrorMessage(string message)
		{
			ClearLine(Console.GetCursorPosition().Top);
			Console.WriteLine($"❌ {message}");

			int line = Console.GetCursorPosition().Top - 2;
			ClearLine(line);
		}

		// AI assisted method
		/// <summary>
		/// Colors the string inputted to the given color
		/// </summary>
		/// <param name="stringToRecolor">The string that should get the new color</param>
		/// <param name="colorToUse">The color to use</param>
		/// <returns>A ANSI colored string</returns>
		public string GetColoredString(string stringToRecolor, ConsoleColor colorToUse)
		{
			int colorCode = GetAnsiColorCode(colorToUse);
			return $"\u001b[38;5;{colorCode}m{stringToRecolor}\u001b[0m";
		}

		// AI assisted method
		// I've used AI here because my old approach with the console colors didn't work
		// ConsoleColor is mixed up, making red to blue and cyan to yellow
		private int GetAnsiColorCode(ConsoleColor color)
		{
			return color switch
			{
				ConsoleColor.Black => 0,
				ConsoleColor.DarkRed => 1,
				ConsoleColor.DarkGreen => 2,
				ConsoleColor.DarkYellow => 3,
				ConsoleColor.DarkBlue => 4,
				ConsoleColor.DarkMagenta => 5,
				ConsoleColor.DarkCyan => 6,
				ConsoleColor.Gray => 7,
				ConsoleColor.DarkGray => 8,
				ConsoleColor.Red => 9,
				ConsoleColor.Green => 10,
				ConsoleColor.Yellow => 11,
				ConsoleColor.Blue => 12,
				ConsoleColor.Magenta => 13,
				ConsoleColor.Cyan => 14,
				ConsoleColor.White => 15,
				_ => 7 // Default to gray
			};
		}
	}
}
