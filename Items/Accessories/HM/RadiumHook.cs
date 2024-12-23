﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	internal class RadiumHook : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radium Digger Hook");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(1800);
			base.item.shootSpeed = 20f;
			base.item.shoot = ModContent.ProjectileType<RadiumHookProjectile>();
		}
	}
}
