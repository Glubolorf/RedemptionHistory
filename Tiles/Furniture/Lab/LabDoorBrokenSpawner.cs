using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Lab
{
	public class LabDoorBrokenSpawner : PlaceholderTile
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
			base.DisplayName.SetDefault("Broken Lab Door (Spawner)");
			base.Tooltip.SetDefault("Spawns Infected Scientists\n[c/ff0000:Unbreakable (500% Pickaxe Power)]");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createTile = ModContent.TileType<LabDoorBrokenTile>();
		}
	}
}
