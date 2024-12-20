using System;
using Redemption.Tiles.Furniture.AncientWood;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.AncientWood
{
	public class AncientWoodPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Platform");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 14;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<AncientWoodPlatformTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 1);
			modRecipe.AddTile(null, "AncientWoodWorkbenchTile");
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
