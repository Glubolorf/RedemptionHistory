using System;
using Redemption.Tiles.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class AncientStone : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gathic Gladestone");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = 0;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<AncientStoneTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientStoneWall", 4);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "AncientStone2", 10);
			modRecipe2.AddIngredient(62, 1);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 10);
			modRecipe2.AddRecipe();
		}
	}
}
