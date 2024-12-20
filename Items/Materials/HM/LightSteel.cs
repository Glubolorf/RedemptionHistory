using System;
using Redemption.Tiles.Bars;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class LightSteel : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hikarite Alloy");
			base.Tooltip.SetDefault("'Also known as Light Steel, a strong and light alloy with Holy properties'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 8;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<LightSteelBarTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1225, 2);
			modRecipe.AddIngredient(null, "PureIron", 2);
			modRecipe.AddIngredient(1508, 1);
			modRecipe.AddTile(133);
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
