using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class KaniteBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Bar");
			base.Tooltip.SetDefault("'Has potential...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 0, 4, 0);
			base.item.rare = 0;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = base.mod.TileType("KaniteBarTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteOre", 2);
			modRecipe.AddTile(17);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
