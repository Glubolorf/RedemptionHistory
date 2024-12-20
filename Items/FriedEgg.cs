using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class FriedEgg : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fried Egg");
			base.Tooltip.SetDefault("'Because eggs are tasty.'\nMinor improvements to all stats");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item2;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.maxStack = 30;
			base.item.consumable = true;
			base.item.width = 12;
			base.item.height = 38;
			base.item.value = 100;
			base.item.rare = 1;
			base.item.buffType = 26;
			base.item.buffTime = 3600;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ChickenEgg", 1);
			modRecipe.AddTile(96);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
