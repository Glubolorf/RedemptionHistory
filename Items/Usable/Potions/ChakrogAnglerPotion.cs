using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class ChakrogAnglerPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Angler Potion");
			base.Tooltip.SetDefault("You emit bright light while submerged\nClears the Soulless Cavern's waters\nIncreased damage while submerged");
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
			base.item.height = 26;
			base.item.maxStack = 30;
			base.item.value = Item.sellPrice(0, 0, 85, 0);
			base.item.rare = 11;
			base.item.buffType = ModContent.BuffType<AnglerPotionBuff>();
			base.item.buffTime = 36000;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.alchemy = true;
			modRecipe.AddIngredient(null, "ChakrogAngler", 1);
			modRecipe.AddIngredient(126, 1);
			modRecipe.AddTile(13);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
