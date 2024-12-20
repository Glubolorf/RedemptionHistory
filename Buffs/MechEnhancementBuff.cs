using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class MechEnhancementBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Mechanical Enhancement");
			base.Description.SetDefault("\"Nanobots are enchancing your fighting abilities\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.magicDamage *= 1.08f;
			player.meleeDamage *= 1.08f;
			player.minionDamage *= 1.08f;
			player.rangedDamage *= 1.08f;
			player.thrownDamage *= 1.08f;
			druidDamagePlayer.druidDamage *= 1.08f;
			player.moveSpeed += 25f;
		}
	}
}
