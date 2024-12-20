using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class LilyBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Lily Empowerment");
			base.Description.SetDefault("\"Increased druidic damage, decreased all other damage\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.3f;
			player.magicDamage -= 0.4f;
			player.meleeDamage -= 0.4f;
			player.minionDamage -= 0.4f;
			player.rangedDamage -= 0.4f;
			player.thrownDamage -= 0.4f;
		}
	}
}
