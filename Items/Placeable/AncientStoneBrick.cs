using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class AncientStoneBrick : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Stone Brick");
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
			base.item.value = 0;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<AncientStoneBrickTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientStone", 2);
			modRecipe.AddTile(17);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "AncientStoneBrickWall", 4);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
