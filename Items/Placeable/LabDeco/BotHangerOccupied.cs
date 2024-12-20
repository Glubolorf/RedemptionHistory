using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class BotHangerOccupied : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Occupied Bot Hanger");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 46;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 6;
			base.item.createTile = base.mod.TileType("BotHangerOccupiedTile");
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 12);
			modRecipe.AddRecipeGroup("Redemption:Plating", 4);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 8);
			modRecipe.AddIngredient(null, "XenomiteShard", 12);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
