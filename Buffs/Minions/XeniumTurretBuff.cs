﻿using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class XeniumTurretBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Xenium Autoturret");
			base.Description.SetDefault("\"Pewpewpewpewpewpew\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<XeniumTurretMinion>()] > 0)
			{
				modPlayer.xeniumMinion = true;
			}
			if (!modPlayer.xeniumMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
