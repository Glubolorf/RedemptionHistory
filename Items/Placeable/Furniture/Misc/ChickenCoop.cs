using System;
using Redemption.Tiles.Furniture.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class ChickenCoop : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chicken Coop");
			base.Tooltip.SetDefault("Occasionally spawns chicken eggs");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 42;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 1;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 0, 15, 0);
			base.item.createTile = ModContent.TileType<ChickenCoopTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddRecipeGroup("Redemption:Chicken", 2);
			modRecipe.AddIngredient(9, 30);
			modRecipe.AddIngredient(1727, 10);
			modRecipe.AddTile(106);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
