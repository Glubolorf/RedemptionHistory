﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian23Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Icy Pixie");
			base.Description.SetDefault("\"An Icy Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			modPlayer.rapidStave = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("NatureGuardian23")] > 0)
			{
				modPlayer2.natureGuardian23 = true;
			}
			if (!modPlayer2.natureGuardian23)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}