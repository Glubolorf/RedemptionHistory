using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class LabRecMonitor : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Reception Monitors");
		}

		public override void SetDefaults()
		{
			base.item.width = 48;
			base.item.height = 38;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<ReceptionDeskMonitorsTile>();
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 18);
			modRecipe.AddRecipeGroup("Redemption:Plating", 4);
			modRecipe.AddIngredient(null, "LabCeilingMonitor", 5);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
