using System;
using Redemption.Tiles.Furniture.Shade;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Shade
{
	public class ShadestonePlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadestone Platform");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 16;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<ShadestonePlatformTile>();
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Shadestone", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
