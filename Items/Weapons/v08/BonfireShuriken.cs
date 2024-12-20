using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class BonfireShuriken : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bonfire Shuriken");
			base.Tooltip.SetDefault("Shurikens split into flames after a small distance\nNot consumable, but deals less damage to compensate");
		}

		public override void SetDefaults()
		{
			base.item.thrown = true;
			base.item.shootSpeed = 23f;
			base.item.damage = 10;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 24;
			base.item.useTime = 24;
			base.item.width = 30;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.rare = 3;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 1, 0);
			base.item.shoot = ModContent.ProjectileType<BonfireShurikenPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BonfireDagger", 999);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "DruidShuriken", 1);
			modRecipe2.AddIngredient(174, 50);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
