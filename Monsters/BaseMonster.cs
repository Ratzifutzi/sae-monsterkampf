using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monsterkampf.HelperClasses;

namespace Monsterkampf._02_Monsterkampf.Monsters
{
	internal abstract class BaseMonster
	{
		// Base variables
		private float healthPoints = 100;
		private float attackPoints = 15;
		private float defensePoints = 5;
		private float speedPoints = 50;

		// Helper Classes
		ConsoleHelper consoleHelper = new ConsoleHelper();

		// Override variables
		public abstract string MonsterBreed { get; } // This is basically their UID
		public abstract string MonsterIcon { get; } // This is just to prettify the console output
		public abstract ConsoleColor MonsterColor { get; } // The color of the monster displayed in the terminal

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


		//////////////////////////////////////////////////


		/// <summary>
		/// Gets the name of the monster in a prettified way
		/// </summary>
		/// <returns></returns>
		public string GetName()
		{
			return $"{this.MonsterIcon} {this.MonsterBreed}";
		}
		/// <summary>
		/// Gets the colored name of the monster
		/// </summary>
		/// <returns>the name but colored</returns>
		public string GetColoredName()
		{
			return consoleHelper.GetColoredString(this.GetName(), this.MonsterColor);
		}

		/// <summary>
		/// Gets the stats of the monster in an human readable, colored way.
		/// </summary>
		/// <returns>Stats of the monster, colored and human readable</returns>
		public string GetPrettyPrintedStats()
		{
			return $"" +
				$"❤️ {consoleHelper.GetColoredString(this.HP	.ToString(), ConsoleColor.Red)} | " +
				$"⚔️ {consoleHelper.GetColoredString(this.AP.ToString(), ConsoleColor.White)} |" +
				$"🛡️ {consoleHelper.GetColoredString(this.DP.ToString(), ConsoleColor.Blue)} | " +
				$"💨 {consoleHelper.GetColoredString(this.SP.ToString(), ConsoleColor.Gray)}";
		}


		//////////////////////////////////////////////////


		private float calculateAttackDamage(BaseMonster target)
		{
			return Math.Max(this.AP - target.DP, 0);
		}
		/// <summary>
		/// Deals damage to the target while taking stats like DP into consideration.
		/// </summary>
		/// <param name="target">The monster to attack</param>
		public virtual void Attack(BaseMonster target)
		{
			Console.WriteLine($"⌛ {this.GetColoredName()} tries to strike an attack on {target.GetColoredName()}...");

			// Checks if the attack is valid
			if (target == this) { Console.WriteLine("❌ But they realized that they could not harm themselves..."); return; };
			if (target.MonsterBreed == this.MonsterBreed) { Console.WriteLine("❌ But realised he cannot harm his own breed..."); return; };

			// Calculate the attack amount
			float damageToDeal = calculateAttackDamage(target);

            Console.WriteLine($"💔 The attack on {target.GetColoredName()} was successful and dealt " +
				$"{consoleHelper.GetColoredString(damageToDeal.ToString(), ConsoleColor.Red)} damage.");

			// Deal damage and hurt shield
			target.HP -= damageToDeal;
			if(target.DP > 0) { target.DP -= 1; };
		}
	}
}
