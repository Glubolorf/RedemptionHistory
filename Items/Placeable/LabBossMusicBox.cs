using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class LabBossMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Music Box (Abandoned Lab Minibosses)");
			base.Tooltip.SetDefault("Harry101UK - Boreal Defendant");
		}

		public override void SetDefaults()
		{
			base.item.useStyle = 1;
			base.item.useTurn = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.autoReuse = true;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<LabBossMusicBoxTile>();
			base.item.width = 32;
			base.item.height = 32;
			base.item.rare = 4;
			base.item.value = 100000;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(576, 1);
			modRecipe.AddIngredient(null, "LabPlating", 20);
			modRecipe.AddIngredient(null, "Xenomite", 8);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
