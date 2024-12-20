using System;
using Redemption.Tiles.Plants;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Plants
{
	public class IcarsFlower : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icar's Flower");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 38;
			base.item.value = 7500;
			base.item.rare = 7;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<IcarsFlowerTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "MysteriousFlowerPetal", 12);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
