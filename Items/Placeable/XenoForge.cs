using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class XenoForge : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xeno Forge");
			base.Tooltip.SetDefault("Forges Xenomite");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 30;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 1000;
			base.item.rare = 7;
			base.item.createTile = ModContent.TileType<XenoForgeTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(221, 1);
			modRecipe.AddIngredient(null, "Xenomite", 4);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
