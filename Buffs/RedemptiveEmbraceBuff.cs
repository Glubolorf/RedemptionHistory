using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class RedemptiveEmbraceBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Redemptive Embrace");
			base.Description.SetDefault("\"Face your sins!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 25;
			player.jumpBoost = true;
			player.endurance += 0.1f;
			player.moveSpeed += 300f;
			player.nightVision = true;
			player.statLifeMax2 += 200;
			player.statManaMax2 += 200;
			player.lifeRegen += 50;
			player.manaRegen += 50;
			player.noKnockback = true;
		}
	}
}
