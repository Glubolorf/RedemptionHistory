using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LifeFruitOfThorns : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fruit of Thorns");
			base.Tooltip.SetDefault("'Useless without a source of energy'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 46;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThornsF", 12);
			modRecipe.AddIngredient(1291, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
