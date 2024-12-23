﻿using System;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class RootTendril : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Root Tendril");
			base.Tooltip.SetDefault("Shoots out a root tendril");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 24;
			base.item.value = Item.buyPrice(0, 0, 45, 0);
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.knockBack = 6.5f;
			base.item.damage = 14;
			base.item.noUseGraphic = true;
			base.item.shoot = ModContent.ProjectileType<CursedRootFlailPro1>();
			base.item.shootSpeed = 30f;
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.melee = true;
			base.item.autoReuse = true;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().thornCrown)
			{
				flat += 600f;
			}
		}
	}
}
