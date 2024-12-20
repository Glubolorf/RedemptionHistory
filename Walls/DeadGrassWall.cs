﻿using System;
using Terraria.ModLoader;

namespace Redemption.Walls
{
	public class DeadGrassWall : PlaceholderTile
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
			base.DisplayName.SetDefault("Petrified Grass Wall");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.item.createWall = ModContent.WallType<DeadGrassWallTile>();
		}
	}
}
