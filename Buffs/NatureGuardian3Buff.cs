﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian3Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Corrupt Pixie");
			base.Description.SetDefault("\"A Corrupt Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.staveStreamShot = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian3")] > 0)
			{
				modPlayer2.natureGuardian3 = true;
			}
			if (!modPlayer2.natureGuardian3)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}