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
		public List<Type> IndexMonsterTypes()
		{
			// The next 3 lines of code were LLM assisted but written down by me and slightly modified.
			var baseType = typeof(BaseMonster);
			var derivedTypes = Assembly.GetExecutingAssembly().GetTypes()
			.Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));

			return derivedTypes.ToList();
		}

		/// <summary>
		/// Gets the monster types and turns it into a dictionary
		/// </summary>
		/// <returns>A dictionary where the key is the type and the value is the colored name</returns>
		public Dictionary<BaseMonster, string> GetMonsterDictionary()
		{
			// Get all types
			List<Type> allMonsterTypes = IndexMonsterTypes();
			Dictionary<BaseMonster, string> output = new Dictionary<BaseMonster, string>();

			// Iterate through all types and create an instance for every type
			foreach (Type monsterType in allMonsterTypes)
            {
				// Create the non typed instance
				object? instance = Activator.CreateInstance(monsterType);

				// Make sure the instance is a base monster or else just ignore it and continue
				// This also creates the typedInstance variable
				if (instance is not BaseMonster typedInstance) continue;

				// Add it to the output dict that gets returned at the end
				output.Add(typedInstance, typedInstance.GetColoredName()); 
            }

            return output;
		}	
	}
}
