using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class RadioactiveIceWall : PlaceholderTile
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
			base.DisplayName.SetDefault("Radioactive Ice Wall");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<RadioactiveIceWallTile>();
		}
	}
}
