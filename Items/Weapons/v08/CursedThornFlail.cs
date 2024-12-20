﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedThornFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Tendril");
			base.Tooltip.SetDefault("Shoots out a cursed tendril");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.value = Item.buyPrice(0, 50, 0, 0);
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.knockBack = 7.5f;
			base.item.damage = 930;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("CursedThornFlailPro");
			base.item.shootSpeed = 30f;
			base.item.UseSound = SoundID.Item1;
			base.item.melee = true;
			base.item.autoReuse = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().thornCrown)
			{
				flat += 50f;
			}
		}
	}
}
