using System;
using Redemption.Walls;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class LabPlatingWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Panel Wall");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 7;
			base.item.value = Item.buyPrice(0, 0, 1, 0);
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.rare = 6;
			base.item.createWall = ModContent.WallType<LabWallTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 4);
			modRecipe.AddRecipe();
		}
	}
}
