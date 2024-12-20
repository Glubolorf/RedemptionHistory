using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class LivingDeadLeavesWall : PlaceholderTile
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
			base.DisplayName.SetDefault("Living Petrified Leaves Wall");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<LivingDeadLeavesWallTile>();
		}
	}
}
