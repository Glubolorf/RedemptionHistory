using System;
using Redemption.NPCs;
using Redemption.Tiles.Wasteland;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class RedeGlobalTile : GlobalTile
	{
		public override void RandomUpdate(int i, int j, int type)
		{
			if (type == 2 && !Main.dayTime && RedeWorld.downedThorn && Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.tile[i, j - 1].liquid == 0 && Main.tile[i, j - 1].wall == 0 && Main.rand.Next(400) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<NightshadeTile>(), true, false, -1, 0);
			}
			if (type == 2 && Main.dayTime && RedeWorld.downedThorn && !Framing.GetTileSafely(i, j - 1).active() && !Framing.GetTileSafely(i, j - 2).active() && !Framing.GetTileSafely(i + 1, j - 1).active() && !Framing.GetTileSafely(i + 1, j - 2).active() && Main.tile[i, j].active() && Main.tile[i + 1, j].active() && Main.tile[i, j - 1].liquid == 0 && Main.tile[i, j - 1].wall == 0 && Main.rand.Next(6000) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<AnglonicMysticBlossomTile>(), true, false, -1, 0);
			}
			if (type == 2 && Main.dayTime && !Framing.GetTileSafely(i, j - 1).active() && !Framing.GetTileSafely(i, j - 2).active() && !Framing.GetTileSafely(i + 1, j - 1).active() && !Framing.GetTileSafely(i + 1, j - 2).active() && Main.tile[i, j].active() && Main.tile[i + 1, j].active() && Main.tile[i, j - 1].liquid == 0 && Main.tile[i, j - 1].wall == 0 && (RedeWorld.downedThorn ? (Main.hardMode ? (Main.rand.Next(6000) == 0) : (Main.rand.Next(2500) == 0)) : (Main.rand.Next(500) == 0)))
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<HeartOfThornsTile>(), true, false, -1, 0);
			}
			if ((type == ModContent.TileType<DeadGrassTileCorruption>() || type == ModContent.TileType<IrradiatedEbonstoneTile>() || type == ModContent.TileType<DeadGrassTileCrimson>() || type == ModContent.TileType<IrradiatedCrimstoneTile>()) && Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.tile[i, j - 1].liquid == 0 && Main.rand.Next(300) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<RadRootTile>(), true, false, -1, 0);
			}
		}

		public override bool Drop(int i, int j, int type)
		{
			Player player = Main.player[Main.myPlayer];
			if (Main.netMode != 1 && !WorldGen.noTileActions && !WorldGen.gen && type == 5 && Main.tile[i, j + 1].type == 2 && Main.rand.Next(6) == 0)
			{
				Projectile.NewProjectile((float)(i * 16), (float)((j - 10) * 16), (float)(-4 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(-3, 0)), ModContent.ProjectileType<TreeBugFall>(), 0, 0f, 255, 0f, 0f);
			}
			return (type != ModContent.TileType<AncientDirtTile>() || !TileID.Sets.BreakableWhenPlacing[ModContent.TileType<AncientDirtTile>()]) && base.Drop(i, j, type);
		}
	}
}
