using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Misc
{
	public class HangingTied : PlaceholderTile
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
			base.DisplayName.SetDefault("Hanging Tied");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<HangingTiedTile>();
		}
	}
}
