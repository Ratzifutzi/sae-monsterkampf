using Monsterkampf._02_Monsterkampf.Monsters;

namespace Monsterkampf._02_Monsterkampf
{
	internal class Program
	{
		static void Main(string[] args)
		{
			OrkMonster myOrk = new OrkMonster();

			for (int i = 0; i < 15; i++)
			{
				Console.WriteLine(myOrk.HP);
				myOrk.HP -= 10;
			}
		}
	}
}
