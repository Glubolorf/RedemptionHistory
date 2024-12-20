using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class AncientWoodBed : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Bed");
			base.Tooltip.SetDefault("'Uncomfortable and feels rough...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 14;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 2000;
			base.item.createTile = base.mod.TileType("AncientWoodBedTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 15);
			modRecipe.AddIngredient(225, 5);
			modRecipe.AddTile(null, "AncientWoodWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
