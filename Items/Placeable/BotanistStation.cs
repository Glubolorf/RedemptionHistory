using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class BotanistStation : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Botanist Station");
			base.Tooltip.SetDefault("Used to combine Seedbags");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 32;
			base.item.height = 40;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 6;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.createTile = base.mod.TileType("BotanistStationTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(398, 1);
			modRecipe.AddIngredient(549, 5);
			modRecipe.AddIngredient(548, 5);
			modRecipe.AddIngredient(547, 5);
			modRecipe.AddIngredient(base.mod.ItemType("SoulOfBloom"), 15);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
