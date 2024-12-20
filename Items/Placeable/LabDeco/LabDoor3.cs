using System;
using Redemption.Tiles.LabDeco;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class LabDoor3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Small Laboratory Door");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 32;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 1500;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<LabDoor3TileClosed>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 7);
			modRecipe.AddIngredient(170, 2);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
