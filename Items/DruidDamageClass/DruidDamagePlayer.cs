using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidDamagePlayer : ModPlayer
	{
		public static DruidDamagePlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<DruidDamagePlayer>();
		}

		public override void ResetEffects()
		{
			this.ResetVariables();
		}

		public override void UpdateDead()
		{
			this.ResetVariables();
		}

		private void ResetVariables()
		{
			this.druidDamage = 1f;
			this.druidKnockback = 0f;
			this.druidCrit = 0;
		}

		public float druidDamage = 1f;

		public float druidKnockback;

		public int druidCrit;
	}
}
