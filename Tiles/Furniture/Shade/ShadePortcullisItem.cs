using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadePortcullisItem : PlaceholderTile
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
			base.DisplayName.SetDefault("Shade Portcullis");
			base.Tooltip.SetDefault("Opens and closes with wire\n[c/ff0000:Unbreakable (500% Pickaxe Power)]");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<ShadePortcullisClose>();
		}
	}
}
