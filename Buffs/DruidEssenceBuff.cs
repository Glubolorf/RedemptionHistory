using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class DruidEssenceBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Druidic Essence");
			base.Description.SetDefault("\"Increased druidic damage and crit\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage *= 1.05f;
			druidDamagePlayer.druidCrit += 5;
		}
	}
}
