using System;
using Redemption.Tiles.Furniture.Shade;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Shade
{
	public class ShadeSink : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Sink");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 2000;
			base.item.createTile = ModContent.TileType<ShadeSinkTile>();
			base.item.placeStyle = 0;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Shadestone", 6);
			modRecipe.AddIngredient(206, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
