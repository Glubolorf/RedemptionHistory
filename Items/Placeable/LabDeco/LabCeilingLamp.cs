using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class LabCeilingLamp : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Ceiling Lamp");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 32;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 9000;
			base.item.rare = 6;
			base.item.createTile = base.mod.TileType("LabCeilingLampTile");
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 6);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 6);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
