using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	internal class KaniteHook : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Hook");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(1236);
			base.item.shootSpeed = 16f;
			base.item.shoot = ModContent.ProjectileType<KaniteHookProjectile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 15);
			modRecipe.AddIngredient(85, 4);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
