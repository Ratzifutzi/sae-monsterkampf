using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf._02_Monsterkampf.Monsters
{
	internal class OrkMonster : BaseMonster
	{
		public override string MonsterBreed { get; } ="Ork";
		public override string MonsterIcon { get; } = "🧌";
		public override ConsoleColor MonsterColor { get; } = ConsoleColor.DarkGreen;

		public OrkMonster()
		{
			HP = 10;
			AP = 10;
			DP = 5;
			SP = 25;
		}
	}
}
