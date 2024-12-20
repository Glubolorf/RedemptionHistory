using System;
using Redemption.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class LabPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Panel");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = Item.buyPrice(0, 0, 2, 0);
			base.item.consumable = true;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<LabTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2860, 66);
			modRecipe.AddIngredient(3460, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 66);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LabPlatingWall", 4);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
