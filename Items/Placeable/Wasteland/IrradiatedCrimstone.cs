using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Wasteland
{
	public class IrradiatedCrimstone : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Irradiated Crimstone");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = Item.buyPrice(0, 0, 1, 0);
			base.item.consumable = true;
			base.item.createTile = base.mod.TileType("IrradiatedCrimstoneTile");
		}
	}
}
