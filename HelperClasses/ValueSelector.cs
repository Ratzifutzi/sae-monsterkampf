using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf.HelperClasses
{
	public class ValueSelector
	{
		private void ClearLine(int line)
		{
			Console.SetCursorPosition(0, line);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(0, line);
		}

		// Instance required modules
		ConsoleHelper consoleHelper = new ConsoleHelper();

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
				if (input == null) { consoleHelper.SendErrorMessage("Please enter at least something."); continue; };
				if (!int.TryParse(input, out int parsedInput)) { consoleHelper.SendErrorMessage("Please enter a number"); continue; };
				if (choices.Count < parsedInput) { consoleHelper.SendErrorMessage("Please enter the number corresponding to your selection without the dot."); continue; };
				if (parsedInput <= 0) { consoleHelper.SendErrorMessage("Please enter the number corresponding to your selection without the dot."); continue; };

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
