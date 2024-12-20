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
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.lifeSteal1 = true;
			modPlayer.staveQuadShot = true;
			druidDamagePlayer.druidDamage += 0.25f;
			druidDamagePlayer.druidCrit += 25;
			player.lifeRegen += 15;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian21")] > 0)
			{
				modPlayer2.natureGuardian21 = true;
			}
			if (!modPlayer2.natureGuardian21)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
