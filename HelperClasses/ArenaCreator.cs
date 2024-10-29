using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Monsterkampf._02_Monsterkampf.Monsters;

namespace Monsterkampf.HelperClasses
{
	internal class ArenaCreator
	{
		private Dictionary<string, Dictionary<string, float>> arenaSettings = new()
		{
			["1v1"] = new Dictionary<string, float>
			{
				["MaxTeamSize"] = 1,
				["MaxTeams"] = 2
			},
			["TDM"] = new Dictionary<string, float>
			{
				["MaxTeamSize"] = 25,
				["MaxTeams"] = 25
			},
		};

		// These values will be used when the arena is being created.
		// Key is the stat and the value is the question that should be asked
		// The code will append a " (Default: int)" at the end of the string.
		private Dictionary<string, string> changeableMonsterParameters = new()
		{
			["HP"] = "❤️ How much HP should the monster have?",
			["AP"] = "⚔️ How much base attack points should the monster have?",
			["DP"] = "🛡️ How much base defense points should the monster have?",
			["SP"] = "💨 How fast should the monster be?",
		};

		/// <summary>
		/// Creates an empty arena
		/// </summary>
		/// <returns>An empty arena</returns>
		private List<List<BaseMonster>> CreateEmptyArena()
		{
			return new List<List<BaseMonster>>();
		}

		/// <summary>
		/// Initializes an army and reads user input
		/// </summary>
		/// <param name="arenaType">A string that allows either "1v1" or "TDM"</param>
		/// <returns>A full army or null if setup is invalid.</returns>
		/// <exception cref="Exception">Army type is not recognized.</exception>
		public List<List<BaseMonster>>? InitializeArena(string arenaType)
		{
			// Check if the arena type is valid
			if (!arenaSettings.ContainsKey(arenaType)) throw new Exception($"Arena type '{arenaType}' is not valid.");
			Console.WriteLine($"Now creating a {arenaType} game...");

			var settings = arenaSettings[arenaType];
			float maxTeamSize = settings["MaxTeamSize"];
			float maxTeams = settings["MaxTeams"];

			// Instance Helpers
			ValueSelector valueSelector = new ValueSelector();
			MonsterClassIndex monsterIndex = new MonsterClassIndex();
			ConsoleHelper consoleHelper = new ConsoleHelper();

			// Create the arena
			var arena = this.CreateEmptyArena();

			// Create all the teams
			for (int teamNumber = 0; teamNumber < maxTeams; teamNumber++)
			{
				for (int i = 0; i < maxTeamSize; i++)
				{
					List<string> selectionOptions = [];

					// Initialize a new list for this team
					arena.Add(new List<BaseMonster>());

					// Get all monsters
					var monsters = monsterIndex.GetMonsterDictionary().Keys.ToList();

					// Remove monsters that are already in a different team
					foreach (var iTeam in arena)
					{
						if (teamNumber == arena.IndexOf(iTeam)) continue;

						foreach (var iMonster in iTeam)
						{
							monsters.Remove(iMonster.GetColoredName());
						}
					}

					// Check if there are still any monsters to add
					// Or if the user messed it up and there is a reset needed
					// This is responsible for the error message, the code that
					// calls the creator will still need to restart the process.
					if (monsters.Count == 0)
					{
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("Hmm, seems like you've messed up something.");

						Console.ForegroundColor = ConsoleColor.DarkYellow;
						Console.WriteLine("Monsters of the same breed will not fight against eachother,");
						Console.WriteLine("but if you continue this team setup, a fair fight would not be possible.");
						Console.WriteLine("Please take more care next time. The setup will be restarted.\n");

						Console.ForegroundColor = ConsoleColor.Gray;
						return null;
					}

					// Add the updated monsters to the selection options
					selectionOptions.InsertRange(selectionOptions.Count, monsters);

					// Add the finish team option if this is not the first option
					if (i > 0) { selectionOptions.Add("Finish this team"); }

					// Add the finish army option if this is the 3rd or higher army
					if (teamNumber > 0 && i > 0) { selectionOptions.Add("Finish the whole setup"); }

					int selectedIndex = valueSelector.Create(
						$"What monster do you want to add to the team #{teamNumber + 1}?",
						selectionOptions
					);

					// Break out of this loop if the finish option was picked
					if (selectedIndex == monsters.Count + 0) { break; }
					if (selectedIndex == monsters.Count + 1) { return arena; }

					// Create instance of the selected monster
					var selectedMonsterName = selectionOptions[selectedIndex];
					BaseMonster instancedMonster = monsterIndex.GetMonsterDictionary()[selectedMonsterName];

					// Start the stats selector
					foreach (var pair in changeableMonsterParameters)
					{
						string statName = pair.Key;
						string baseQuestion = pair.Value;

						PropertyInfo? statInfo = typeof(BaseMonster).GetProperty(statName);
						if (statInfo == null) throw new Exception();

						var defaultValue = statInfo.GetValue(instancedMonster);

						// Print the question and the default value
						Console.WriteLine($"{baseQuestion} (Default: {defaultValue})");
						
						// Loop to validate input
						bool validInput = false;
						while (validInput == false)
						{
							// Read user input
							string? rawUserInput = Console.ReadLine();

							// Just use the default value if the input is empty (User just only ENTER)
							if(rawUserInput == "")
							{
								int currentLine = Console.GetCursorPosition().Top - 1;

								validInput = true;
								statInfo.SetValue(instancedMonster, defaultValue);

								Console.SetCursorPosition(0, currentLine);
                                Console.WriteLine(defaultValue);

								continue;
							}

							// Validate checks
							if (!int.TryParse(rawUserInput, out int result)) { consoleHelper.SendErrorMessage("Please enter a number"); continue; };
							if (Math.Clamp(result, 0, 100) != result) { consoleHelper.SendErrorMessage("Please make sure that your input is in the range of 0-100"); continue; }
							if (statName == "AP" && result == 0) { consoleHelper.SendErrorMessage("AP must be greather than 0"); continue; }

							// Set the selected value
							statInfo.SetValue(instancedMonster, result);

							validInput = true;
						}
					}

					// Add the monster to the arena and the correct team
					arena[teamNumber].Add(instancedMonster);
				}
			}

			return arena;
		}

		public void PrettyPrintArena(List<List<BaseMonster>> arenaToPrint)
		{
			foreach (var team in arenaToPrint)
			{
				int teamNumber = arenaToPrint.IndexOf(team);

				if (team.Count == 0) continue;
				Console.WriteLine($"┌ Team #{teamNumber + 1}");

				foreach (BaseMonster monster in team)
				{
					int monsterNumber = team.IndexOf(monster);

					if (monsterNumber == team.Count - 1)
					{
						Console.WriteLine($"└ {monster.GetColoredName()} {monster.GetPrettyPrintedStats()}");
					}
					else
					{
						Console.WriteLine($"├ {monster.GetColoredName()} {monster.GetPrettyPrintedStats()}");
					}
				}

				if (team.Count != 0) Console.WriteLine("");
			}
		}
	}
}
