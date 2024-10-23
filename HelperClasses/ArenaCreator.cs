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
				["MaxTeamSize"] = 10,
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

					// Add the cancel option if this is not the first option
					if (i > 0) { selectionOptions.Add("Finish this team"); }

					valueSelector.Create($"What monster do you want to add to the team #{teamNumber}?", selectionOptions);
                }
            }

			return arena;
		}
	}
}
