using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class BileDart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bile Dart");
			base.Tooltip.SetDefault("Decreases target's defense and drains life");
		}

		public override void SetDefaults()
		{
			base.item.damage = 10;
			base.item.ranged = true;
			base.item.width = 10;
			base.item.height = 26;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.rare = 7;
			base.item.knockBack = 2.5f;
			base.item.value = 7;
			base.item.shoot = base.mod.ProjectileType("BileDartPro");
			base.item.shootSpeed = 3f;
			base.item.ammo = AmmoID.Dart;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Bile", 1);
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
