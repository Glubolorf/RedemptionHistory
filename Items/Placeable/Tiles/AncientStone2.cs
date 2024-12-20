using System;
using Redemption.Tiles.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class AncientStone2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gathic Stone");
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
			base.item.createTile = ModContent.TileType<AncientStone2Tile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientStoneWall2", 4);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(3, 1);
			modRecipe2.AddIngredient(172, 5);
			modRecipe2.AddTile(220);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
			ModRecipe modRecipe3 = new ModRecipe(base.mod);
			modRecipe3.AddIngredient(null, "AncientStone", 1);
			modRecipe3.SetResult(this, 1);
			modRecipe3.AddRecipe();
		}
	}
}
