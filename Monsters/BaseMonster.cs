using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf._02_Monsterkampf.Monsters
{
	internal abstract class BaseMonster
	{
		// Base variables
		private float healthPoints = 100;
		private float attackPoints = 15;
		private float defensePoints = 5;
		private float speedPoints = 50;

		// Override variables
		public abstract string MonsterBreed { get; } // This is basically their UID
		public abstract string MonsterIcon { get; } // This is just to prettify the console output

		// Properties
		public float HP
		{
			get { return healthPoints; }
			set { healthPoints = Math.Max(value, 0); }
		}
		public float AP
		{
			get { return attackPoints; }
			set { attackPoints = value; }
		}
		public float DP
		{
			get { return defensePoints; }
			set { defensePoints = Math.Max(value, 0); }
		}
		public float SP
		{
			get { return speedPoints; }
			set { speedPoints = value; }
		}

		/// <summary>
		/// Gets the name of the monster in a prettified way
		/// </summary>
		/// <returns></returns>
		public string GetName()
		{
			return $"{this.MonsterIcon} {this.MonsterBreed}";
		}

		/// <summary>
		/// Deals damage to the target while taking stats like DP into consideration.
		/// </summary>
		/// <param name="target">The monster to attack</param>
		public virtual void Attack(BaseMonster target)
		{
			Console.WriteLine($"⌛ {this.GetName()} tries to strike an attack on {target.GetName()}...");

			// Checks if the attack is valid
			if (target == this) { Console.WriteLine("❌ But they realized that they could not harm themselves..."); return; };
			if (target.MonsterBreed == this.MonsterBreed) { Console.WriteLine("❌ But realised he cannot harm his own breed..."); return; };

			// Calculate in DefensePoints

			target.HP -= AP;
		}
	}
}
