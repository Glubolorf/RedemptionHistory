using System;
using Redemption.Tiles.Furniture.DeadWood;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.DeadWood
{
	public class DeadWoodLamp : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Wood Lamp");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 32;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 150;
			base.item.createTile = ModContent.TileType<DeadWoodLampTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DeadWood", 3);
			modRecipe.AddIngredient(8, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
