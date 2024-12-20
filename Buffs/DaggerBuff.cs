using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class DaggerBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Stabbed");
			base.Description.SetDefault("\"Your damage is increased at the cost of decreased max life\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 -= (int)((float)player.statLifeMax2 * 0.4f);
			player.allDamage += 0.15f;
			player.meleeSpeed += 0.08f;
			player.bleed = true;
		}
	}
}
