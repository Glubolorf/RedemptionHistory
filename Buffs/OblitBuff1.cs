using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class OblitBuff1 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Obliteration Motivation");
			base.Description.SetDefault("\"Increased damage and defense, less life regen\"");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.lifeRegen > 20)
			{
				player.lifeRegen = 20;
			}
			player.allDamage += 0.05f;
			player.statDefense += 4;
			if (player.HasBuff(base.mod.BuffType("OblitBuff2")) || player.HasBuff(base.mod.BuffType("OblitBuff3")) || player.HasBuff(base.mod.BuffType("OblitBuff4")) || player.HasBuff(base.mod.BuffType("OblitBuff5")))
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
