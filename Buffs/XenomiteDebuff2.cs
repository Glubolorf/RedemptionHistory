using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class XenomiteDebuff2 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Heavily Infected");
			base.Description.SetDefault("\"Xenomite is growing in your body...\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -20;
			player.meleeDamage *= 1.5f;
			player.statDefense -= 20;
			player.moveSpeed *= 0.2f;
		}
	}
}
