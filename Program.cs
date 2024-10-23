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
			// Get all monsters that derive from the baseMonster automatically and create lists
			// AI ASSISTED CODE BLOCK START \\
			//var baseType = typeof(BaseMonster);
			//var derivedTypes = Assembly.GetExecutingAssembly().GetTypes()
			//.Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));

			//List<string> allMonsterNames = new List<string>();
			//Dictionary<string, Type> monsterNameToType = new Dictionary<string, Type>();

			//foreach (var type in derivedTypes)
			//{
			//	if (typeof(BaseMonster).IsAssignableFrom(type))
			//	{
			//		var instance = Activator.CreateInstance(type) as BaseMonster;
			//		if (instance != null)
			//		{
			//			allMonsterNames.Add(instance.GetColoredName());
			//			monsterNameToType[instance.GetColoredName()] = type;
			//		}
			//	}
			//}
			// AI ASSISTED CODE BLOCK END \\

			// Setup of the program
			// Set console output encoding to UTF-8 to allow emojis
			Console.OutputEncoding = Encoding.UTF8;

			// Set up the console window
			Console.Title = "Monsterkampf Simulator";

			// Set up the arena
			List<List<BaseMonster>> monsters = new List<List<BaseMonster>>();

            // Get the monsters to fight against eachother
            //BaseMonster firstMonster = 
            Console.WriteLine(new ValueSelector().Create( ["Foo", "Bar", "safafsa"], "Test" ) );

			//monsters[0].Add();

			// Spawn some monsters
			OrkMonster myOrk = new OrkMonster();
			TricksterMonster myTrickster = new TricksterMonster();

			for (int i = 0; i < 15; i++)
			{
				myOrk.Attack(myTrickster);
                Console.WriteLine("");
			}
		}
	}
}
