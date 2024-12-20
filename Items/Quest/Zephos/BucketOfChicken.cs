using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.Zephos
{
	public class BucketOfChicken : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bucket o' Chicken");
			base.Tooltip.SetDefault("'Can't go wrong with a bucket of chicken'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
			base.item.consumable = true;
			base.item.UseSound = SoundID.Item2;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 14;
			base.item.useTime = 14;
			base.item.buffType = 26;
			base.item.buffTime = 72000;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(null, "FriedChicken", 8);
			modRecipe.AddIngredient(9, 6);
			modRecipe.AddIngredient(1073, 1);
			modRecipe.AddIngredient(1098, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
