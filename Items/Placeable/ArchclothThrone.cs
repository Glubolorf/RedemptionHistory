using System;
using Redemption.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class ArchclothThrone : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Archcloth Throne");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 36;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 4;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 30, 0, 0);
			base.item.createTile = ModContent.TileType<ArchclothThroneTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Archcloth", 20);
			modRecipe.AddIngredient(706, 30);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
