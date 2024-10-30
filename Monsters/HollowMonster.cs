using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf._02_Monsterkampf.Monsters
{
	internal class HollowMonster : BaseMonster
	{
		public override string MonsterBreed { get; } ="Hollow";
		public override string MonsterIcon { get; } = "🧟";
		public override ConsoleColor MonsterColor { get; } = ConsoleColor.Cyan;

		public HollowMonster()
		{
			HP = 30;
			AP = 5;
			DP = 0;
			SP = 75;
		}
	}
}
