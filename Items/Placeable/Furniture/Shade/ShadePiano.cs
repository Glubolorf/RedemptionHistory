using System;
using Redemption.Tiles.Furniture.Shade;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Shade
{
	public class ShadePiano : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Piano");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 20;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 2000;
			base.item.createTile = ModContent.TileType<ShadePianoTile>();
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(154, 4);
			modRecipe.AddIngredient(null, "Shadestone", 15);
			modRecipe.AddIngredient(149, 1);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
