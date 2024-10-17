using System.Text;
using Monsterkampf._02_Monsterkampf.Monsters;

namespace Monsterkampf._02_Monsterkampf
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Set console output encoding to UTF-8 to allow emojis
			Console.OutputEncoding = Encoding.UTF8;

			// Spawn some monsters
			OrkMonster myOrk = new OrkMonster();

			for (int i = 0; i < 15; i++)
			{
				myOrk.Attack(myOrk);
                Console.WriteLine("");
			}
		}
	}
}
