using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Misc
{
	public class AncientAltar : PlaceholderTile
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
			base.DisplayName.SetDefault("Ancient Altar");
			base.Tooltip.SetDefault("Gives the Cursed Gem\n[c/ff0000:Unbreakable]");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<AncientAltarTile>();
		}
	}
}
