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
			base.DisplayName.SetDefault("Vendetta Potion");
			base.Tooltip.SetDefault("Attackers also take damage, and get inflicted by poison");
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
			modRecipe.alchemy = true;
			modRecipe.AddIngredient(null, "Nightshade", 2);
			modRecipe.AddIngredient(209, 1);
			modRecipe.AddIngredient(69, 1);
			modRecipe.AddIngredient(276, 1);
			modRecipe.AddIngredient(126, 1);
			modRecipe.AddTile(13);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Nightshade", 2);
			modRecipe.AddIngredient(301, 1);
			modRecipe.AddTile(355);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
