using System;
using Redemption.Tiles.Furniture.DeadWood;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.DeadWood
{
	public class DeadWoodChandelier : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Wood Chandelier");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 44;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 150;
			base.item.createTile = ModContent.TileType<DeadWoodChandelierTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DeadWood", 4);
			modRecipe.AddIngredient(8, 4);
			modRecipe.AddIngredient(85, 1);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
