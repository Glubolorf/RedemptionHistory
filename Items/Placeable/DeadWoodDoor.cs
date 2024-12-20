using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class DeadWoodDoor : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Wood Door");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 32;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 150;
			base.item.createTile = ModContent.TileType<DeadWoodDoorClosed>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DeadWood", 6);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
