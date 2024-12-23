﻿using System;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Druid
{
	public class Petridish : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mitosis");
			base.Tooltip.SetDefault("Throw a Petridish filled with bacteria");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 125;
			base.item.width = 24;
			base.item.height = 20;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.mana = 18;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<PetridishPro>();
			base.item.shootSpeed = 14f;
		}
	}
}
