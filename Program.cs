using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Threading;
using Monsterkampf._02_Monsterkampf.Monsters;
using Monsterkampf.HelperClasses;

namespace Monsterkampf._02_Monsterkampf
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Setup of the program
			// Create helper classes instances
			ValueSelector valueSelector = new ValueSelector();
			MonsterClassIndex monsterIndex = new MonsterClassIndex();
			ArenaCreator arenaCreator = new ArenaCreator();

			// Set console output encoding to UTF-8 to allow emojis
			Console.OutputEncoding = Encoding.UTF8;

			// Set up the console window
			Console.Title = "Monsterkampf Simulator";

			// Help
			valueSelector.Create("Would you like to read the introduction?", ["Yes", "No"]);

			////////////////////////////////////////

			// Select the gamemode
			List<String> arenaModes = [
				"1v1",
				"TDM"
			];

			int arenaModeSelection = valueSelector.Create("What gamemode do you want to play?", arenaModes);

			bool creationSuccessful = false;
			var arena = arenaCreator.InitializeArena(arenaModes[arenaModeSelection]);
			if (arena != null) { creationSuccessful = true; };

			while (!creationSuccessful)
			{
				arenaCreator.InitializeArena(arenaModes[arenaModeSelection]);

				if(arena != null) { creationSuccessful = true; };
			}

            Console.WriteLine("\nPerfect! Here is a brief overview of the arena:");
			arenaCreator.PrettyPrintArena(arena);
			//valueSelector.Create("What monster should join the fight first?", monsterIndex.GetMonsterDictionary().Values.ToList());
		}
	}
}
