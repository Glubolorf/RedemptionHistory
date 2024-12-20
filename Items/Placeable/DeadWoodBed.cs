using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class DeadWoodBed : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Wood Bed");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 2000;
			base.item.createTile = ModContent.TileType<DeadWoodBedTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DeadWood", 15);
			modRecipe.AddIngredient(225, 5);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
