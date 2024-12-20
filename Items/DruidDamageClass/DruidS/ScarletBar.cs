using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.DruidS
{
	public class ScarletBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scarlet Bar");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 0, 30, 0);
			base.item.rare = 6;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ScarlionOre", 3);
			modRecipe.AddTile(133);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
