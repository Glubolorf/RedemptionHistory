using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LightSteel : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hikarite Alloy");
			base.Tooltip.SetDefault("'Also known as Light Steel, a strong and light alloy with Holy properties'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 8;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1225, 2);
			modRecipe.AddIngredient(null, "PureIron", 2);
			modRecipe.AddIngredient(1508, 1);
			modRecipe.AddTile(133);
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
