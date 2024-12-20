using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class GathicCryoFurnace : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gathic Cryo-Furnace");
			base.Tooltip.SetDefault("'Legendary blacksmiths of Gathuram used this to create their strongest weapons...'\nUsed to make Pure-Iron");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 26;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 4;
			base.item.consumable = true;
			base.item.value = Item.buyPrice(0, 3, 50, 0);
			base.item.createTile = base.mod.TileType("GathicCryoFurnaceTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientStone", 20);
			modRecipe.AddIngredient(2503, 4);
			modRecipe.AddIngredient(null, "GathicCryoCrystal", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
