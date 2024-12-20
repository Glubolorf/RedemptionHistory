using System;
using Terraria;

namespace Redemption.Items.DruidDamageClass.DruidS
{
	public class ScarlionOre : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scarlion Ore");
			base.Tooltip.SetDefault("'Blood Ruby'");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 8, 0);
			base.item.rare = 6;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = base.mod.TileType("ScarlionOreTile");
		}
	}
}
