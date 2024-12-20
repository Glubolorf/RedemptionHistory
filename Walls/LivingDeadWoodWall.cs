using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class LivingDeadWoodWall : PlaceholderTile
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
			base.DisplayName.SetDefault("Living Petrified Wood Wall");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<LivingDeadWoodWallTile>();
		}
	}
}
