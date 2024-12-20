using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Containers
{
	public class LabChestLocked2 : PlaceholderTile
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
			base.DisplayName.SetDefault("Special Locked Lab Chest");
			base.Tooltip.SetDefault("Requires a special Keycard");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<LabChestTileLocked2>();
		}
	}
}
