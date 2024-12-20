using System;
using Redemption.Tiles.Bars;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class ScarletBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scarlet Bar");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 0, 30, 0);
			base.item.rare = 6;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<ScarletBarTile>();
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ScarlionOre", 3);
			modRecipe.AddTile(133);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
