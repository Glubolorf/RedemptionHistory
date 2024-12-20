using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class HardenedRadioactiveSandWall : PlaceholderTile
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
			base.DisplayName.SetDefault("Hardened Radioactive Sand Wall");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<HardenedRadioactiveSandWallTile>();
		}
	}
}
