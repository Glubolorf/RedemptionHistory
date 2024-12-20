using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class BotHanger : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Empty Bot Hanger");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 44;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<BotHangerEmptyTile>();
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 12);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 8);
			modRecipe.AddIngredient(null, "XenomiteShard", 12);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
