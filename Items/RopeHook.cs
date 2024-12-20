using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	internal class RopeHook : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rope Hook");
			base.Tooltip.SetDefault("Affected by gravity");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(1236);
			base.item.shootSpeed = 16f;
			base.item.shoot = base.mod.ProjectileType("RopeHookProjectile");
			base.item.value = Item.buyPrice(0, 1, 25, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(118, 1);
			modRecipe.AddIngredient(985, 2);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
