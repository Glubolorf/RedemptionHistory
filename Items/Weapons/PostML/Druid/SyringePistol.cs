﻿using System;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Druid
{
	public class SyringePistol : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Syringe Pistol");
			base.Tooltip.SetDefault("'Give them a taste of your own medicine!'\nFires syringes that stick onto the target");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 22f;
			base.item.crit = 84;
			base.item.damage = 920;
			base.item.knockBack = 5f;
			base.item.useStyle = 5;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.width = 40;
			base.item.height = 26;
			base.item.maxStack = 1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item99;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.shoot = ModContent.ProjectileType<NeedlePro>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BluePrints", 1);
			modRecipe.AddIngredient(null, "DNAgger", 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
