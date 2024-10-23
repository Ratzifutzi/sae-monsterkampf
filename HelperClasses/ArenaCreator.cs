using System;
using System.Collections.Generic;
using System.Linq;
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
			}
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
		/// <returns>A full army</returns>
		/// <exception cref="Exception">Army type is not recognized.</exception>
		public List<List<BaseMonster>> InitializeArena(string arenaType)
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

			// Create the arena
			var arena = this.CreateEmptyArena();

			// Create all the teams
			for (int teamNumber = 0; teamNumber < maxTeams; teamNumber++)
			{
                for (int i = 0; i < maxTeamSize; i++)
                {
					List<string> selectionOptions = [];

					// Get all monsters
					var monsters = monsterIndex.GetMonsterDictionary().Values.ToList();
					selectionOptions.InsertRange(selectionOptions.Count, monsters);

					// Add the finish team option if this is not the first option
					if (i > 0) { selectionOptions.Add("Finish this team"); }

					// Add the finish army option if this is the 3rd or higher army
					if (teamNumber > 1) { selectionOptions.Add("Finish the army"); }

					int selectedIndex = valueSelector.Create(
						$"What monster do you want to add to the team #{teamNumber + 1}?",
						selectionOptions
					);

					// Break out of this loop if the finish option was picked
					if (selectedIndex == monsters.Count + 0) { break; }
					if (selectedIndex == monsters.Count + 1) { return arena; }
				}
            }

			return arena;
		}
	}
}
