using System;
using Redemption.Tiles.MusicBoxes;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.MusicBoxes
{
	public class KeeperBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Music Box (The Keeper)");
			base.Tooltip.SetDefault("SpectralAves - Haunting Loneliness");
		}

		public override void SetDefaults()
		{
			base.item.useStyle = 1;
			base.item.useTurn = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.autoReuse = true;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<KeeperBoxTile>();
			base.item.width = 32;
			base.item.height = 26;
			base.item.rare = 4;
			base.item.value = 100000;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(576, 1);
			modRecipe.AddIngredient(null, "LostSoul", 4);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
