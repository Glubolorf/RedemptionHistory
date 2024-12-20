using System;
using Redemption.Tiles.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class DeadWood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Wood");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = 0;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<DeadWoodTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DeadWoodWall", 4);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
