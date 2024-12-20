using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class MoltenScrap : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Molten Scrap");
			base.Tooltip.SetDefault("'Careful! It's hot'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 28;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 50, 0);
			base.item.rare = 11;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(67, 60, true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CyberPlating", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Mk1Capacitator", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Mk1Plating", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Mk2Capacitator", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Mk2Plating", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Mk3Capacitator", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Mk3Plating", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ScrapMetal", 1);
			modRecipe.AddTile(null, "LabForgeTile");
			modRecipe.SetResult(this, 6);
			modRecipe.AddRecipe();
		}
	}
}
