﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian15Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("True Lunar Statuette");
			base.Description.SetDefault("\"A True Lunar Statuette is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.rapidStave = true;
			modPlayer.staveQuadShot = true;
			player.nightVision = true;
			player.detectCreature = true;
			player.dangerSense = true;
			player.manaCost *= 0.5f;
			if (!Main.dayTime)
			{
				player.moveSpeed += 20f;
				player.jumpBoost = true;
			}
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian15")] > 0)
			{
				modPlayer2.natureGuardian15 = true;
			}
			if (!modPlayer2.natureGuardian15)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}