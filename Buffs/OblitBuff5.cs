using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class OblitBuff5 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Obliteration Motivation");
			base.Description.SetDefault("\"Increased damage and defense, less life regen\"");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.lifeRegen > 0)
			{
				player.lifeRegen = 0;
			}
			player.allDamage += 0.25f;
			player.statDefense += 20;
		}
	}
}
