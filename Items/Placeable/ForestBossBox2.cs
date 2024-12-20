using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class ForestBossBox2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Music Box (Ancient Deity Duo)");
			base.Tooltip.SetDefault("Antti Martikainen Music - Taivaantuli");
		}

		public override void SetDefaults()
		{
			base.item.useStyle = 1;
			base.item.useTurn = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.autoReuse = true;
			base.item.consumable = true;
			base.item.createTile = base.mod.TileType("ForestBossBox2Tile");
			base.item.width = 32;
			base.item.height = 24;
			base.item.rare = 4;
			base.item.value = 100000;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(576, 1);
			modRecipe.AddIngredient(null, "AncientPowerCore", 5);
			modRecipe.AddIngredient(null, "CursedThorns", 5);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
