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
			ArenaHelper arenaHelper = new ArenaHelper();
			ConsoleHelper consoleHelper = new ConsoleHelper();

			// Set console output encoding to UTF-8 to allow emojis
			Console.OutputEncoding = Encoding.UTF8;

			// Set up the console window
			Console.Title = "Monsterkampf Simulator";

			////////////////////////////////////////

			// Select the gamemode
			List<String> arenaModes = [
				"1v1",
				"TDM"
			];

			int arenaModeSelection = valueSelector.Create("What gamemode do you want to play?", arenaModes);

			bool creationSuccessful = false;
			var arena = new List<List<BaseMonster>>();

			while (!creationSuccessful)
			{
				var createdArena = arenaCreator.InitializeArena(arenaModes[arenaModeSelection]);

				if (createdArena != null) { creationSuccessful = true; arena = createdArena; };
			}

			/////////////////////////////////////////////////////
			// Test Arena Start
			//arena.Add(new List<BaseMonster>());
			//arena.Add(new List<BaseMonster>());

			//OrkMonster ork = new OrkMonster();
			//TricksterMonster trickster = new TricksterMonster();

			//arena[0].Add(ork);
			//arena[0].Add(new OrkMonster());
			//arena[1].Add(trickster);
			// Test Arena End
			/////////////////////////////////////////////////////

			Console.WriteLine("\nPerfect! Here is a brief overview of the arena:");
			arenaHelper.PrettyPrintArena(arena);
			Console.WriteLine("Press any key to start the fight!");

			////////////////////////////////////////
			Console.ReadKey();
			Console.Clear();
			////////////////////////////////////////
			Int64 turnsPlayed = 0;

			while (true)
			{
				// Setup the next turn
				turnsPlayed += 1;
				Console.Clear();

				// Print the header
				Console.WriteLine(consoleHelper.GetColoredString($"--- // ROUND #{turnsPlayed} \\\\ ---\n", ConsoleColor.Magenta));

				// Sort the monsters depending on their speed stat
				var monsters = arenaHelper.GetArenaParticipants(arena);
				var sortedMonsters = arenaHelper.SortMonstersBySP(monsters);

				bool roundEnded = false;
				foreach (BaseMonster attacker in sortedMonsters)
				{
					// Check if the attacker is still alive
					if (attacker.HP <= 0) continue;

					// Try to get a random opponent
					BaseMonster? target = null;
					try
					{
						target = arenaHelper.GetRandomOpponent(arena, attacker);
					}
					catch
					{
						// If we can't get an opponent, it means the battle is over
						roundEnded = true;
						break;
					}

					// Attack the target
					attacker.Attack(target);

					// Check if the target died
					if (target.HP <= 0)
					{
						// Let the user know
						Console.WriteLine($"💀 The {target.GetColoredName()} has {consoleHelper.GetColoredString("died", ConsoleColor.Red)} from the attack...");

						// Remove them from the arena
						arenaHelper.RemoveMonsterFromArena(arena, target);
					}

					// Empty line + a small delay
					Console.WriteLine("");
				}

				// Print the current game arena(the state) at the footer
				Console.WriteLine(""); // Whitespace for spacing
				arenaHelper.PrettyPrintArena(arena);

				// If there is only one team left, end the battle
				if (arenaHelper.CountAliveTeams(arena) <= 1) roundEnded = true;

				// Check if the round ended and if so, end the loop
				if (roundEnded || arenaHelper.CountArenaParticipants(arena) <= 1) break;

				// Wait for user input before starting new round
				Console.WriteLine("Press any key to continue the fight...");
				Console.ReadKey();
			}

			// Wait for user input one last time
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.Clear();

            // Pronounce the winner team
			Console.WriteLine(consoleHelper.GetColoredString($"--- // FIGHT LASTED {turnsPlayed} ROUNDS \\\\ ---\n", ConsoleColor.Magenta));
            Console.WriteLine("The winner team is:");
			arenaHelper.PrettyPrintArena(arena);
		}
	}
}
