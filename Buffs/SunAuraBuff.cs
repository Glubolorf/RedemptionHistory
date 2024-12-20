using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SunAuraBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Sun Aura");
			base.Description.SetDefault("\"Empowered by ancient lihzahrd powers\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.magicDamage *= 1.15f;
			player.meleeDamage *= 1.15f;
			player.minionDamage *= 1.15f;
			player.rangedDamage *= 1.15f;
			player.thrownDamage *= 1.15f;
			druidDamagePlayer.druidDamage *= 1.15f;
			player.lifeRegen += 8;
		}
	}
}
