using System;
using Redemption.Tiles.Furniture.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	internal class BoneSpiderTerrarium : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone Spider Cage");
			base.Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 0, 0);
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<BoneSpiderTerrariumTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BoneSpiderItem", 1);
			modRecipe.AddIngredient(2208, 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
