using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class DruidicAltar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druidic Altar");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nUsed to make Druid equipment");
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 34;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 3;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 0, 60, 50);
			base.item.createTile = base.mod.TileType("DruidicAltarTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddIngredient(19, 6);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddIngredient(706, 6);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
