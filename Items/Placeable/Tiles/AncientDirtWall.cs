using System;
using Redemption.Walls;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class AncientDirtWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Dirt Wall");
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
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createWall = ModContent.WallType<AncientDirtWallTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientDirt", 1);
			modRecipe.SetResult(this, 4);
			modRecipe.AddRecipe();
		}
	}
}
