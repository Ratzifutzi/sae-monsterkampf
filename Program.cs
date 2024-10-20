using System.Text;
using Monsterkampf._02_Monsterkampf.Monsters;

namespace Monsterkampf._02_Monsterkampf
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// All Mosters
			List<string> allMonsterNames = [
				new OrkMonster().GetColoredName(),
				new TricksterMonster().GetColoredName()
			];

			// Setup of the program
			// Set console output encoding to UTF-8 to allow emojis
			Console.OutputEncoding = Encoding.UTF8;

			// Set up the console window
			Console.Title = "Monsterkampf Simulator";


			// Get the monsters to fight against eachother
			new ValueSelector().Create(allMonsterNames);

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
