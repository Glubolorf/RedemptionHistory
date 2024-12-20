using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Natural
{
	public class ShadeCystItem : PlaceholderTile
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
			base.DisplayName.SetDefault("Shade Cyst");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<ShadeCyst>();
		}
	}
}
