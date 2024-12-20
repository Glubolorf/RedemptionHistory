using System;
using Redemption.Tiles.Ores;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class VesselFrag : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vessel Fragment");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 0, 85, 0);
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<MasksTile>();
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
