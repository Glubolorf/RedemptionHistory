using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class RevivalPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Revival Potion");
			base.Tooltip.SetDefault("Use on an unconsious town npc to wake them up");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 38;
			base.item.maxStack = 30;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.alchemy = true;
			modRecipe.AddIngredient(null, "AnglonicMysticBlossom", 1);
			modRecipe.AddIngredient(5, 4);
			modRecipe.AddIngredient(23, 8);
			modRecipe.AddIngredient(126, 4);
			modRecipe.AddTile(13);
			modRecipe.SetResult(this, 4);
			modRecipe.AddRecipe();
		}
	}
}
