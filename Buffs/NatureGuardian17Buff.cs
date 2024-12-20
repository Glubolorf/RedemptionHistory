using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian17Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("True Holy Statuette");
			base.Description.SetDefault("\"He watches...\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>();
			player.nightVision = true;
			player.statDefense += 18;
			player.endurance *= 0.12f;
			player.noFallDmg = true;
			player.noKnockback = true;
			player.lifeRegen += 5;
			player.statLifeMax2 += 50;
			if (Main.dayTime)
			{
				player.moveSpeed += 20f;
				player.jumpBoost = true;
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian17")] > 0)
			{
				modPlayer.natureGuardian17 = true;
			}
			if (!modPlayer.natureGuardian17)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 36000;
		}
	}
}
