using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class HealShroomBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Spores: Heal Shroom");
			base.Description.SetDefault("\"Increased Life Regen\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen++;
		}
	}
}
