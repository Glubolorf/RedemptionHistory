using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Lab
{
	public class LabBossDoorTile1HItem : PlaceholderTile
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
			base.DisplayName.SetDefault("Horizontal Reinforced Lab Door");
			base.Tooltip.SetDefault("Closes when any of the Lab Minibosses/Bosses are active\n[c/ff0000:Unbreakable]");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<LabBossDoorTile1H>();
		}
	}
}
