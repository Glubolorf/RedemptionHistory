using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class SewerHole : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sewer Hole");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 6;
			base.item.createTile = base.mod.TileType("SewerHoleTile");
			base.item.placeStyle = 0;
		}
	}
}
