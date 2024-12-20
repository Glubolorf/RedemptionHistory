using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class RedeGlobalTile : GlobalTile
	{
		public override void RandomUpdate(int i, int j, int type)
		{
			if (type == 2 && !Main.dayTime && Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.rand.Next(300) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("NightshadeTile"), true, false, -1, 0);
			}
			if (type == base.mod.TileType("DeadGrassTileCorruption") && type == base.mod.TileType("IrradiatedEbonstoneTile") && type == base.mod.TileType("DeadGrassTileCrimson") && type == base.mod.TileType("IrradiatedCrimstoneTile") && Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.rand.Next(300) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("RadRootTile"), true, false, -1, 0);
			}
		}

		public override bool Drop(int i, int j, int type)
		{
			Player player = Main.player[Main.myPlayer];
			if (Main.netMode != 1 && !WorldGen.noTileActions && !WorldGen.gen && type == 5 && Main.tile[i, j + 1].type == 2 && Main.rand.Next(6) == 0)
			{
				Projectile.NewProjectile((float)(i * 16), (float)((j - 10) * 16), (float)(-4 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(-3, 0)), base.mod.ProjectileType("TreeBugFall"), 0, 0f, 255, 0f, 0f);
			}
			return base.Drop(i, j, type);
		}
	}
}
