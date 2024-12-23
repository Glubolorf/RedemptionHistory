﻿using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardian12Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Lava Guardian");
			base.Description.SetDefault("\"A Lava Guardian is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer3 = player.GetModPlayer<RedePlayer>();
			modPlayer3.staveSpeed += 0.35f;
			modPlayer3.moltenEruption = true;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian12>()] > 0)
			{
				modPlayer2.natureGuardian12 = true;
			}
			if (!modPlayer2.natureGuardian12)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
