using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.Daerel
{
	public class UltimatePotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ultimate Potion");
			base.Tooltip.SetDefault("'Wouldn't recommend drinking... Seriously.'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 18));
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 34;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
			base.item.consumable = true;
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.noUseGraphic = true;
			base.item.useTurn = true;
			base.item.useAnimation = 14;
			base.item.useTime = 14;
			base.item.buffType = ModContent.BuffType<DisgustingDebuff>();
			base.item.buffTime = 3000;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "NightshadePotion", 1);
			modRecipe.AddIngredient(227, 1);
			modRecipe.AddIngredient(303, 1);
			modRecipe.AddIngredient(304, 1);
			modRecipe.AddIngredient(2329, 1);
			modRecipe.AddIngredient(2346, 1);
			modRecipe.AddIngredient(292, 1);
			modRecipe.AddIngredient(299, 1);
			modRecipe.AddIngredient(289, 1);
			modRecipe.AddIngredient(290, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
