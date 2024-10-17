using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf._02_Monsterkampf.Monsters
{
	internal abstract class BaseMonster
	{
		// Base Propeties
		private float healthPoints = 100;
		private float attackPoints = 15;
		private float defensePoints = 5;
		private float speedPoints = 50;

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
		/// Deals damage to the target while taking stats like DP into consideration.
		/// </summary>
		/// <param name="target">The monster to attack</param>
		public virtual void Attack(BaseMonster target)
		{
			target.HP -= AP;
		}
	}
}
