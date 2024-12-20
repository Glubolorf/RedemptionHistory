﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian18Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Tree of Creation");
			base.Description.SetDefault("\"A Tree of Creation is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer3 = player.GetModPlayer<RedePlayer>();
			player.endurance *= 0.1f;
			player.noKnockback = true;
			player.lifeRegen += 15;
			player.manaRegen += 15;
			player.statLifeMax2 += 50;
			player.statManaMax2 += 50;
			player.moveSpeed += 50f;
			player.longInvince = true;
			modPlayer3.staveSpeed += 0.35f;
			modPlayer3.staveQuadShot = true;
			modPlayer3.staveScatterShot = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian18")] > 0)
			{
				modPlayer2.natureGuardian18 = true;
			}
			if (!modPlayer2.natureGuardian18)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
