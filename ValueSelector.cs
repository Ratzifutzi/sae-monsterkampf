using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf
{
    public class ValueSelector
    {
		public static void ClearLine(int line)
		{
			Console.SetCursorPosition(0, line);
			Console.Write(new string(' ', Console.WindowWidth) );
			Console.SetCursorPosition(0, line);
		}

		private void SendErrorMessage()
		{
            Console.WriteLine("❌ Please enter the number corresponding to your selection without the dot.");
		}

        public string Create(List<string> choices)
        {
			string? choice = null;

			// Print the options into the console
			Console.WriteLine("❓ Please select one of the options and enter the number assigned to it:");
			for (int i = 0; i < choices.Count; i++)
			{
				Console.WriteLine($"   {i + 1}. {choices[i]}");
			}

			while (choice == null)
			{
				string? input = Console.ReadLine();

				// Parse the input
				if (input == null) { SendErrorMessage(); continue; };

				if (choices.Contains(input)) { return input; }
				if (!int.TryParse(input, out int parsedInput)) { SendErrorMessage(); continue; };
				if (choices.Count < parsedInput) { SendErrorMessage(); continue; };
				if (parsedInput <= 0) { SendErrorMessage(); continue; };

				// If all checks passed, make the choice to the parsedInput
				choice = choices[parsedInput - 1];

			}

			return choice;
		}
    }
}
