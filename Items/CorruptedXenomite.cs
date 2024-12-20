using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CorruptedXenomite : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Xenomite");
			base.Tooltip.SetDefault("'Infects mechanical things...'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 10;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 5);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddTile(null, "CorruptorTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
