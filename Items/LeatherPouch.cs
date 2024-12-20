using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LeatherPouch : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Leather Pouch");
			base.Tooltip.SetDefault("Used to craft Seedbags");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.maxStack = 99;
			base.item.value = Item.buyPrice(0, 0, 1, 50);
			base.item.rare = 0;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(259, 2);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
