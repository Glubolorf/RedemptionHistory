using System;
using Redemption.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class Mk1MicrobotFactory : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-1 Microbot Factory");
			base.Tooltip.SetDefault("Right-clicking will construct a Mk-1 Microbot\nShoots weak, rapid-fire lasers that don't pierce");
		}

		public override void SetDefaults()
		{
			base.item.damage = 15;
			base.item.noMelee = true;
			base.item.summon = true;
			base.item.width = 42;
			base.item.height = 40;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.rare = 5;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.createTile = ModContent.TileType<Mk1MicrobotFactoryTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(21, 20);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(null, "Mk1Capacitator", 2);
			modRecipe.AddIngredient(null, "Mk1Plating", 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(705, 20);
			modRecipe2.AddIngredient(null, "AIChip", 1);
			modRecipe2.AddIngredient(null, "Mk1Capacitator", 2);
			modRecipe2.AddIngredient(null, "Mk1Plating", 6);
			modRecipe2.AddTile(134);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
