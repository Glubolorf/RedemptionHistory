using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class SlayerStation : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cyber Fabricator");
			base.Tooltip.SetDefault("Used to craft from Cyberscrap\nFound on Slayer's Crashed Ship");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 34;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = (base.item.value = Item.sellPrice(0, 8, 0, 0));
			base.item.rare = 9;
			base.item.createTile = base.mod.TileType("SlayerFabricatorTile");
			base.item.placeStyle = 0;
		}
	}
}
