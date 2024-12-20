using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Shade
{
	public class WardenAltarItem : PlaceholderTile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Placeholder";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Warden's Altar");
			base.Tooltip.SetDefault("[c/ff0000:Unbreakable]");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<WardenAltar>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
