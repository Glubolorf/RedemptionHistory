using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Lab
{
	public class DeadHazmatDeco : PlaceholderTile
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
			base.DisplayName.SetDefault("Dead Hazmat Corpse");
			base.Tooltip.SetDefault("Gives Crowbar and Hazmat Suit\n[c/ff0000:Unbreakable (500% Pickaxe Power)]");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<DeadHazmatTile>();
		}
	}
}
