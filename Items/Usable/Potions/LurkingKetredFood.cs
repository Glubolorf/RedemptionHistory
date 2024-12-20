using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class LurkingKetredFood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Deep Dwell Dish");
			base.Tooltip.SetDefault("'Tastes like [REDACTED]'\nMinor improvements to all stats");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item2;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.maxStack = 30;
			base.item.consumable = true;
			base.item.width = 32;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 0, 25, 0);
			base.item.rare = 11;
			base.item.buffType = 26;
			base.item.buffTime = 14400;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LurkingKetred", 1);
			modRecipe.AddTile(96);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "ChakrogAngler", 1);
			modRecipe2.AddTile(96);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
