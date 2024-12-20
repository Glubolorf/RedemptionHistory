using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class OblitBuff4 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Obliteration Motivation");
			base.Description.SetDefault("\"Increased damage and defense, less life regen\"");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.lifeRegen > 2)
			{
				player.lifeRegen = 2;
			}
			player.allDamage += 0.2f;
			player.statDefense += 16;
			if (player.HasBuff(ModContent.BuffType<OblitBuff5>()))
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
