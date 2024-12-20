using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class DangerTapeWall : PlaceholderTile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Danger Tape Wall");
			base.Tooltip.SetDefault("[c/ff0000:Unbreakable]");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<DangerTapeWallTile>();
		}
	}
}
