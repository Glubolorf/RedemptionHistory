﻿using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class HiveGrowthBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Hive Growth");
			base.Description.SetDefault("\"Hive Growths to fight for you!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.lightPet[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<RedePlayer>().infectedHeart = true;
			bool projHas = player.ownedProjectileCounts[ModContent.ProjectileType<HiveGrowthFriendly>()] <= 0;
			if (player.whoAmI == Main.myPlayer && projHas)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<HiveGrowthFriendly>(), 200, 1f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
