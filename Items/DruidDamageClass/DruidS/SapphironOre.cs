using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.DruidS
{
	public class SapphironOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sapphiron Ore");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Sky Sapphire'");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 8, 0);
			base.item.rare = 6;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = base.mod.TileType("SapphironOreTile");
		}
	}
}
