﻿using System;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NatureGuardianBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Nature Pixie");
			base.Description.SetDefault("\"A Nature Pixie is protecting you!\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.GetModPlayer<RedePlayer>().staveSpeed += 0.35f;
			RedePlayer modPlayer2 = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<NatureGuardian1>()] > 0)
			{
				modPlayer2.natureGuardian1 = true;
			}
			if (!modPlayer2.natureGuardian1)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
