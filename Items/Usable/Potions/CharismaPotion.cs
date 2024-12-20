using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class CharismaPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Charisma Potion");
			base.Tooltip.SetDefault("Shops have lower prices\nEnemies drop more gold");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.width = 32;
			base.item.height = 30;
			base.item.maxStack = 30;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 3;
			base.item.buffType = ModContent.BuffType<CharismaPotionBuff>();
			base.item.buffTime = 36000;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.alchemy = true;
			modRecipe.AddIngredient(2308, 1);
			modRecipe.AddIngredient(null, "Nightshade", 1);
			modRecipe.AddIngredient(315, 1);
			modRecipe.AddIngredient(126, 1);
			modRecipe.AddTile(13);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(2308, 1);
			modRecipe2.AddIngredient(null, "Nightshade", 1);
			modRecipe2.AddIngredient(315, 1);
			modRecipe2.AddIngredient(126, 1);
			modRecipe2.AddTile(13);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
