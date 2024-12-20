using System;
using Redemption.Tiles.Furniture.Shade;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Shade
{
	public class ShadeClock : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Clock");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 38;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 2000;
			base.item.createTile = ModContent.TileType<ShadeClockTile>();
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyIronBar = true;
			modRecipe.AddIngredient(22, 3);
			modRecipe.AddIngredient(170, 6);
			modRecipe.AddIngredient(null, "Shadestone", 10);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
