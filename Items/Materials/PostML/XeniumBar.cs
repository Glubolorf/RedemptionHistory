using System;
using Redemption.Tiles.Bars;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class XeniumBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Bar");
			base.Tooltip.SetDefault("'Condensed starlite and xenomite'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(3, 12));
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 11;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<XeniumBarTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "RawXenium", 3);
			modRecipe.AddIngredient(null, "MoltenScrap", 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
