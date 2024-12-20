using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class OblitBuff3 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Obliteration Motivation");
			base.Description.SetDefault("\"Increased damage and defense, less life regen\"");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.lifeRegen > 5)
			{
				player.lifeRegen = 5;
			}
			player.allDamage += 0.15f;
			player.statDefense += 12;
			if (player.HasBuff(base.mod.BuffType("OblitBuff4")) || player.HasBuff(base.mod.BuffType("OblitBuff5")))
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
