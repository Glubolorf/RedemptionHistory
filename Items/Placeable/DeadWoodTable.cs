using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class DeadWoodTable : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Wood Table");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 42;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 500;
			base.item.createTile = ModContent.TileType<DeadWoodTableTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DeadWood", 8);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
