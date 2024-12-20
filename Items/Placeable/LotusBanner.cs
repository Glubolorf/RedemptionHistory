using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class LotusBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lotus Rune Banner");
			base.Tooltip.SetDefault("'A powerful banner, proof of a true Druid'\n30% druid crit chance when nearby\n40% reduced crit chance for all other damage types when nearby");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 52;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.rare = 7;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 20000;
			base.item.createTile = base.mod.TileType("LotusBannerTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(225, 25);
			modRecipe.AddIngredient(null, "SoulOfBloom", 30);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
