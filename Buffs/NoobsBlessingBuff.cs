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
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.magicDamage *= 1.1f;
			player.meleeDamage *= 1.1f;
			player.rangedDamage *= 1.1f;
			player.minionDamage *= 1.1f;
			player.thrownDamage *= 1.1f;
			druidDamagePlayer.druidDamage *= 1.1f;
		}
	}
}
