using System;
using Redemption.Tiles.LabDeco;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class LabBed : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hospital Bed");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 6;
			base.item.consumable = true;
			base.item.value = 6000;
			base.item.createTile = ModContent.TileType<LabBedTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 12);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 8);
			modRecipe.AddIngredient(225, 8);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
