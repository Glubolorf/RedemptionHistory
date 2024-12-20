using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class NightshadePotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Potion");
			base.Tooltip.SetDefault("Gain stealth and increased damage reduction at night\nDoes nothing at day");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.width = 20;
			base.item.height = 32;
			base.item.maxStack = 30;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 1;
			base.item.buffType = base.mod.BuffType("NightshadePotionBuff");
			base.item.buffTime = 10800;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Nightshade", 2);
			modRecipe.AddIngredient(315, 1);
			modRecipe.AddIngredient(2303, 1);
			modRecipe.AddIngredient(126, 1);
			modRecipe.AddTile(13);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Nightshade", 2);
			modRecipe.AddIngredient(315, 1);
			modRecipe.AddIngredient(2303, 1);
			modRecipe.AddIngredient(126, 1);
			modRecipe.AddTile(355);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Nightshade", 2);
			modRecipe.AddIngredient(2346, 1);
			modRecipe.AddTile(355);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
