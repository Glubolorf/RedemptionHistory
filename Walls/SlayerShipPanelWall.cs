using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class SlayerShipPanelWall : PlaceholderTile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Slayer's Ship Wall Panel");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<SlayerShipPanelWallTile>();
		}
	}
}
