﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian25Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Pixie Trinity");
			base.Description.SetDefault("\"All Nature Pixie are protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.rapidStave = true;
			modPlayer.staveStreamShot = true;
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 15;
			player.resistCold = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian25")] > 0)
			{
				modPlayer2.natureGuardian25 = true;
			}
			if (!modPlayer2.natureGuardian25)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}