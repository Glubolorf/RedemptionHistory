using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class AncientHallPillarItem : PlaceholderTile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Hall Pillar");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<AncientHallPillarWall>();
		}
	}
}
