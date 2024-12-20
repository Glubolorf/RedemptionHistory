using System;
using Redemption.Tiles.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class AncientWood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = 50;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<AncientWoodTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWoodWall", 4);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
