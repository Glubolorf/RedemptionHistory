using System;
using Redemption.Walls;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class AncientWoodWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Wall");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 32;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 7;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createWall = ModContent.WallType<AncientWoodWallTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 1);
			modRecipe.SetResult(this, 4);
			modRecipe.AddRecipe();
		}
	}
}
