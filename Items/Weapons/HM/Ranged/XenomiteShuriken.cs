using System;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Ranged
{
	public class XenomiteShuriken : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Shuriken");
			base.Tooltip.SetDefault("Bounces when hitting an enemy");
		}

		public override void SetDefaults()
		{
			base.item.thrown = true;
			base.item.shootSpeed = 23f;
			base.item.damage = 51;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 14;
			base.item.useTime = 14;
			base.item.width = 46;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.rare = 7;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.shoot = ModContent.ProjectileType<XenomiteShurikenPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XenomiteKunai", 999);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "DruidShuriken", 1);
			modRecipe2.AddIngredient(null, "XenomiteShard", 50);
			modRecipe2.AddTile(134);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
