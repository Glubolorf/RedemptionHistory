using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SpiceGreenBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Earthly Spices");
			base.Description.SetDefault("\"Slightly increased druid damaged and crit chance\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 6;
		}
	}
}
