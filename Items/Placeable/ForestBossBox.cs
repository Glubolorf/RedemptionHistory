using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class ForestBossBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Music Box (Cursed Beings of the Forest)");
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
			base.item.createTile = base.mod.TileType("ForestBossBoxTile");
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
			modRecipe.AddIngredient(null, "AncientWood", 40);
			modRecipe.AddIngredient(null, "AncientStone", 20);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
