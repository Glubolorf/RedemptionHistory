using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class AnglerPotionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Angler Vision");
			base.Description.SetDefault("\"Increases damage and emits light while submerged\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().anglerPot = true;
			if (player.wet && !player.lavaWet)
			{
				player.allDamage += 0.015f;
				player.accFlipper = true;
				Lighting.AddLight(player.Center, 2f, 2f, 2f);
			}
		}
	}
}
