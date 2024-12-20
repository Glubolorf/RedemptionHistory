using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Lab
{
	public class MossyLabTable : PlaceholderTile
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
			base.DisplayName.SetDefault("Mossy Lab Table");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<MossyLabTableTile>();
		}
	}
}
