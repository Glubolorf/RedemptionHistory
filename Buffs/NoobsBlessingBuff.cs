using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NoobsBlessingBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Newb's Blessing");
			base.Description.SetDefault("Your weapons are blessed by the King of Noobs...");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.allDamage *= 1.1f;
		}
	}
}
