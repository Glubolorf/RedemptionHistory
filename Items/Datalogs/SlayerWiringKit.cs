using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class SlayerWiringKit : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ship Wiring Kit");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 28;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.rare = -11;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.createTile = base.mod.TileType("SlayerWiringKitTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Cyberscrap", 20);
			modRecipe.AddRecipeGroup("Redemption:Plating", 4);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 2);
			modRecipe.AddIngredient(20, 8);
			modRecipe.AddIngredient(530, 15);
			modRecipe.AddTile(null, "SlayerFabricatorTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "Cyberscrap", 20);
			modRecipe2.AddRecipeGroup("Redemption:Plating", 4);
			modRecipe2.AddRecipeGroup("Redemption:Capacitators", 1);
			modRecipe2.AddIngredient(null, "CarbonMyofibre", 2);
			modRecipe2.AddIngredient(703, 8);
			modRecipe2.AddIngredient(530, 15);
			modRecipe2.AddTile(null, "SlayerFabricatorTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
