using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class AncientWoodTable : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Table");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 500;
			base.item.createTile = ModContent.TileType<AncientWoodTableTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 8);
			modRecipe.AddTile(null, "AncientWoodWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
