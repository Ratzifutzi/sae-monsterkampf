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

			////////////////////////////////////////

			// Select the gamemode
			List<String> arenaModes = ["1v1",
				"[N/A] TDM",
				"[N/A] FFA",
			];

			int arenaModeSelection = valueSelector.Create("What gamemode do you want to play?", arenaModes);

			arenaCreator.InitializeArena(arenaModes[arenaModeSelection]);
			//valueSelector.Create("What monster should join the fight first?", monsterIndex.GetMonsterDictionary().Values.ToList());
		}
	}
}
