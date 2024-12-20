using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class ForestCore : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Core");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 26;
			base.item.height = 36;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingTwig", 4);
			modRecipe.AddIngredient(520, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
