using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class AncientForcefieldBuff2 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ancient Forcefield");
			base.Description.SetDefault("\"You are being protected by ancient powers\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.magicDamage *= 1.06f;
			player.meleeDamage *= 1.06f;
			player.minionDamage *= 1.06f;
			player.rangedDamage *= 1.06f;
			player.thrownDamage *= 1.06f;
			druidDamagePlayer.druidDamage *= 1.06f;
			player.endurance += 0.06f;
		}
	}
}
