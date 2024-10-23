using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Monsterkampf._02_Monsterkampf.Monsters;

namespace Monsterkampf.HelperClasses
{
	internal class MonsterClassIndex
	{
		// TODO make static constructor
		/// <summary>
		/// Indexes all the derived types from the BaseMonster Class
		/// </summary>
		/// <returns>All derived types from the BaseMonster Class</returns>
		public static List<Type> IndexMonsterTypes()
		{
			// The next 3 lines of code were LLM assisted but written down by me and slightly modified.
			var baseType = typeof(BaseMonster);
			var derivedTypes = Assembly.GetExecutingAssembly().GetTypes()
			.Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));

			return derivedTypes.ToList();
		}

		public static Dictionary<string, BaseMonster> GetMonsters()
		{
			// Get all mi
			return new Dictionary<string, BaseMonster>();
		}
	}
}
