using System;
using Redemption.Tiles.MusicBoxes;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.MusicBoxes
{
	public class XenomiteCrystalBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Music Box (Seed of Infection)");
			base.Tooltip.SetDefault("musicman - Virogenesis");
		}

		public override void SetDefaults()
		{
			base.item.useStyle = 1;
			base.item.useTurn = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.autoReuse = true;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<XenomiteCrystalBoxTile>();
			base.item.width = 28;
			base.item.height = 34;
			base.item.rare = 4;
			base.item.value = 100000;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(576, 1);
			modRecipe.AddIngredient(null, "XenomiteShard", 16);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
