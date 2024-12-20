using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class SlayerShipEngine : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ship AFTL Engine");
			base.Tooltip.SetDefault("Stands for Almost-Faster-Than-Light");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 40;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.rare = -11;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.createTile = base.mod.TileType("SlayerShipEngineTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Cyberscrap", 70);
			modRecipe.AddRecipeGroup("Redemption:Plating", 8);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 6);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 8);
			modRecipe.AddIngredient(null, "Plutonium", 20);
			modRecipe.AddIngredient(null, "Uranium", 20);
			modRecipe.AddIngredient(530, 50);
			modRecipe.AddTile(null, "SlayerFabricatorTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
