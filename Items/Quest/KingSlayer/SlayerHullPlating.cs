using System;
using Redemption.Tiles.Furniture.Ship;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.KingSlayer
{
	public class SlayerHullPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ship Hull Plating");
		}

		public override void SetDefaults()
		{
			base.item.width = 52;
			base.item.height = 44;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = -11;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.createTile = ModContent.TileType<SlayerHullPlatingTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Cyberscrap", 50);
			modRecipe.AddRecipeGroup("Redemption:Plating", 12);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 6);
			modRecipe.AddTile(null, "SlayerFabricatorTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
