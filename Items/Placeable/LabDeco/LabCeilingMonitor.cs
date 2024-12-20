using System;
using Redemption.Tiles.LabDeco;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class LabCeilingMonitor : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Ceiling Monitor");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<CeilingMonitorTile>();
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 6);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 8);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
