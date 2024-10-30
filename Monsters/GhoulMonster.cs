using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf._02_Monsterkampf.Monsters
{
	internal class GhoulMonster : BaseMonster
	{
		public override string MonsterBreed { get; } ="Ghoul";
		public override string MonsterIcon { get; } = "👻";
		public override ConsoleColor MonsterColor { get; } = ConsoleColor.White;

		public GhoulMonster()
		{
			HP = 1;
			AP = 50;
			DP = 0;
			SP = 100;
		}
	}
}
