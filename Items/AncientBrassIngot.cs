using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AncientBrassIngot : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Bar");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 0, 0, 50);
			base.item.rare = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = base.mod.TileType("AncientBrassBarTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientBrassChunk", 2);
			modRecipe.AddTile(17);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
