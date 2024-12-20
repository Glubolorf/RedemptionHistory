using System;
using Redemption.Walls;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class AncientStoneWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Stone Wall");
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
			base.item.createWall = ModContent.WallType<AncientStoneWallTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientStone", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 4);
			modRecipe.AddRecipe();
		}
	}
}
