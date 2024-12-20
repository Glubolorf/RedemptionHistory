using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Lab
{
	public class LabCabinetItem : PlaceholderTile
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
			base.DisplayName.SetDefault("Lab Cabinet");
			base.Tooltip.SetDefault("Gives Radiation Pills");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<LabCabinet>();
		}
	}
}
