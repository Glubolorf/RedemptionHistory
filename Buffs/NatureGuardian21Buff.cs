using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
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
			RedePlayer modPlayer3 = player.GetModPlayer<RedePlayer>();
			modPlayer3.lifeSteal1 = true;
			modPlayer3.staveQuadShot = true;
			druidDamagePlayer.druidDamage += 0.25f;
			druidDamagePlayer.druidCrit += 25;
			player.lifeRegen += 2;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian21>()] > 0)
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
