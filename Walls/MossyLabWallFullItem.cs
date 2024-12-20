using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class MossyLabWallFullItem : PlaceholderTile
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
			base.DisplayName.SetDefault("Unsafe Mossy Lab Wall (Full)");
			base.Tooltip.SetDefault("[c/ff0000:Unbreakable]");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<MossyLabWallFull>();
		}
	}
}
