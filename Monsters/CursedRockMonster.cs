using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf._02_Monsterkampf.Monsters
{
	internal class CursedRockMonster : BaseMonster
	{
		public override string MonsterBreed { get; } ="Cursed Rock";
		public override string MonsterIcon { get; } = "🪨";
		public override ConsoleColor MonsterColor { get; } = ConsoleColor.DarkGray;

		public CursedRockMonster()
		{
			HP = 50;
			AP = 2;
			DP = 10;
			SP = 0;
		}
	}
}
