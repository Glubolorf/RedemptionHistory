using System;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class DangerTape2 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Placeable/Tiles/DangerTape";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Danger Tape");
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
			base.item.value = Item.buyPrice(0, 0, 1, 25);
			base.item.consumable = true;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<DangerTape2Tile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 1);
			modRecipe.AddIngredient(1075, 1);
			modRecipe.AddIngredient(1097, 1);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 8);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "DangerTapeWall2", 4);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
