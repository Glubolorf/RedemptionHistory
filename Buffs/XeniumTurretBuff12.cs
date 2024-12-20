﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class XeniumTurretBuff12 : ModBuff
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
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("XeniumTurretMinion12")] > 0)
			{
				modPlayer.xeniumMinion12 = true;
			}
			if (!modPlayer.xeniumMinion12)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}