using System;
using Redemption.Tiles.Furniture.Archcloth;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Archcloth
{
	public class ArchclothBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Archcloth Banner");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.rare = 4;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.createTile = ModContent.TileType<ArchclothBannerTile>();
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Archcloth", 3);
			modRecipe.AddTile(86);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
