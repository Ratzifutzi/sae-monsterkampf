using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf._02_Monsterkampf.Monsters
{
	internal class TricksterMonster : BaseMonster
	{
		public override string MonsterBreed { get; } ="Trickster";
		public override string MonsterIcon { get; } = "🔮";
		public override ConsoleColor MonsterColor { get; } = ConsoleColor.DarkMagenta;
	}
}
