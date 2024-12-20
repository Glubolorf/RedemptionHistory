using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class GraniteAuraBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Granite Aura");
			base.Description.SetDefault("\"You seep with energy\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.magicCrit += 8;
			player.meleeCrit += 8;
			player.rangedCrit += 8;
			player.thrownCrit += 8;
			druidDamagePlayer.druidCrit += 8;
			player.manaRegen += 4;
		}
	}
}
