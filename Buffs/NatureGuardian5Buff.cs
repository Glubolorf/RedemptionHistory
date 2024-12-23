﻿using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian5Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ancient Pixie");
			base.Description.SetDefault("\"An Ancient Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 15;
			p.staveSpeed += 0.35f;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian5>()] > 0)
			{
				modPlayer2.natureGuardian5 = true;
			}
			if (!modPlayer2.natureGuardian5)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
