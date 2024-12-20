using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian21Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Scarlion Spirit");
			base.Description.SetDefault("\"A Scarlion Spirit is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 12;
			player.lifeRegen += 5;
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian21")] > 0)
			{
				modPlayer.natureGuardian21 = true;
			}
			if (!modPlayer.natureGuardian21)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
