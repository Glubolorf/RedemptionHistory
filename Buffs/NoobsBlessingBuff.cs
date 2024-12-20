using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NoobsBlessingBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Fool's Blessing");
			base.Description.SetDefault("Increased damage but decreased defence");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.allDamage *= 1.1f;
			player.statDefense -= 8;
		}
	}
}
