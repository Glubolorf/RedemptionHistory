using System;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class VlitchPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Plating");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = Item.buyPrice(0, 0, 15, 0);
			base.item.consumable = true;
			base.item.rare = 10;
			base.item.createTile = ModContent.TileType<VlitchPlatingTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "VlitchScale", 1);
			modRecipe.AddTile(283);
			modRecipe.SetResult(this, 66);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "VlitchPlatingWall", 4);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
