using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class XenomiteKunai : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Kunai");
			base.Tooltip.SetDefault("Bounces when hitting an enemy");
		}

		public override void SetDefaults()
		{
			base.item.thrown = true;
			base.item.shootSpeed = 21f;
			base.item.crit = 4;
			base.item.damage = 53;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 15;
			base.item.useTime = 15;
			base.item.width = 18;
			base.item.height = 50;
			base.item.maxStack = 999;
			base.item.rare = 7;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 2, 0);
			base.item.shoot = ModContent.ProjectileType<XenomiteKunaiPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DruidDagger", 100);
			modRecipe.AddIngredient(null, "XenomiteShard", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
