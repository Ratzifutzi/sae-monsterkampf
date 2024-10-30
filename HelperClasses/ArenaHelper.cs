using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monsterkampf._02_Monsterkampf.Monsters;

namespace Monsterkampf.HelperClasses
{
	class ArenaHelper
	{
		private static Random random = new Random();

		/// <summary>
		/// When given an arena, this code will pretty print it in an human readable form
		/// </summary>
		/// <param name="arenaToPrint">The arena that should be printed</param>
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

		/// <summary>
		/// Counts the amount of monsters present in the arena
		/// </summary>
		/// <param name="arena">The arena to count</param>
		/// <returns>Amount of monsters in the given arena</returns>
		public int CountArenaParticipants(List<List<BaseMonster>> arena)
		{
			int finds = 0;

			foreach (var team in arena)
			{
				foreach (BaseMonster monster in team)
				{
					finds += 1;
				}
			}

			return finds;
		}

		/// <summary>
		/// Gets all participants of the arena
		/// </summary>
		/// <param name="arena">The arena to search</param>
		/// <returns>All BaseMonsters inside an arena</returns>
		public List<BaseMonster> GetArenaParticipants(List<List<BaseMonster>> arena)
		{
			List<BaseMonster> result = new List<BaseMonster>();

			foreach (var team in arena)
			{
				foreach (BaseMonster monster in team)
				{
					result.Add(monster);
				}
			}

			return result;
		}

		/// <summary>
		/// Sorts all monsters by their SP stat.
		/// Monsters with the same SP stat will be
		/// randomly distributed
		/// </summary>
		/// <param name="monsters">The monsters to sort</param>
		/// <returns>The sorted list</returns>
		public List<BaseMonster> SortMonstersBySP(List<BaseMonster> monsters)
		{
			var result = new List<BaseMonster>();
			result.AddRange(monsters);


			result.Sort((a, b) =>
			{
				if (a.SP != b.SP)
				{
					// Sort by SP in descending order
					return b.SP.CompareTo(a.SP);
				}
				else
				{
					// Randomize order for monsters with the same SP
					return random.Next(-1, 2);
				}
			});

			return result;
		}

		public BaseMonster GetRandomOpponent(List<List<BaseMonster>> arena, BaseMonster attacker)
		{
			var allMonsters = this.GetArenaParticipants(arena);

			// Find the attackers team and remove all monsters from that team
			var attackerTeam = arena.FirstOrDefault(team => team.Contains(attacker)) ?? throw new Exception();

			allMonsters.RemoveAll(monster => attackerTeam.Contains(monster));

			// Get a random item from the remaining monsters
			BaseMonster target = allMonsters[random.Next(0, allMonsters.Count)];

			return target;
		}

		public void RemoveMonsterFromArena(List<List<BaseMonster>> arena, BaseMonster monsterToRemove)
		{
			var team = arena.FirstOrDefault(team => team.Contains(monsterToRemove)) ?? throw new Exception();

			team.Remove(monsterToRemove);
		}

		public int CountAliveTeams(List<List<BaseMonster>> arena)
		{
			int result = 0;
			
			foreach (List<BaseMonster> team in arena)
			{
				if (team.Count > 0) result += 1;
			}

			return result;
		}
	}
}
