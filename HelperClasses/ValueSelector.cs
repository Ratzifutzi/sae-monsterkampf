using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf.HelperClasses
{
	public class ValueSelector
	{
		public static void ClearLine(int line)
		{
			Console.SetCursorPosition(0, line);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(0, line);
		}

		private void SendErrorMessage()
		{
			Console.WriteLine("❌ Please enter the number corresponding to your selection without the dot.");

			int line = Console.GetCursorPosition().Top - 2;
			ClearLine(line);
		}

		/// <summary>
		/// Creates a Selector UI, rendering the given choices with the questions
		/// </summary>
		/// <param name="question">The question to ask, will be rendered at the top</param>
		/// <param name="choices">The choices that should be asked</param>
		/// <returns>The selected key of the choices list</returns>
		public int Create(string question, List<string> choices)
		{
			int choice = -1;

			// Print the options into the console
			Console.WriteLine(question);
			for (int i = 0; i < choices.Count; i++)
			{
				Console.WriteLine($"   {i + 1}. {choices[i]}");
			}

			while (choice == -1)
			{
				string? input = Console.ReadLine();

				// Parse the input
				if (input == null) { SendErrorMessage(); continue; };
				if (!int.TryParse(input, out int parsedInput)) { SendErrorMessage(); continue; };
				if (choices.Count < parsedInput) { SendErrorMessage(); continue; };
				if (parsedInput <= 0) { SendErrorMessage(); continue; };

				// If all checks passed, make the choice to the parsedInput
				choice = parsedInput - 1;

				int inputLine = Console.GetCursorPosition().Top - 1;
				ClearLine(inputLine);
				Console.SetCursorPosition(0, inputLine);

				ConsoleColor oldColor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{choices[choice]}");
				Console.ForegroundColor = oldColor;

				ClearLine(Console.GetCursorPosition().Top);
			}

			return choice;
		}
	}
}
