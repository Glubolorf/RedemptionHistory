using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.NPCs.Critters;
using Redemption.Tiles.Plants;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption
{
	public class RedeGlobalTile : GlobalTile
	{
		public override void NearbyEffects(int i, int j, int type, bool closer)
		{
			Tile topperTile = Framing.GetTileSafely(i, --j);
			if (closer && (Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneXeno || Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneEvilXeno || Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneEvilXeno2 || Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneLab) && topperTile.liquid > 0 && topperTile.liquidType() == 0)
			{
				while (j > 0 && Main.tile[i, j - 1] != null && Main.tile[i, j - 1].liquid > 0 && Main.tile[i, j - 1].liquidType() == 0)
				{
					j--;
				}
				if (Utils.NextBool(Main.rand, 200))
				{
					Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 0, 0, ModContent.DustType<XenoWaterDust>(), Utils.NextFloat(Main.rand, -2f, 2f), Utils.NextFloat(Main.rand, -4f, -2f), 0, default(Color), 1f);
				}
			}
		}

		public override void RandomUpdate(int i, int j, int type)
		{
			if (type == 2 && !Main.dayTime && RedeWorld.downedThorn && !Framing.GetTileSafely(i, j - 1).active() && Main.tile[i, j].active() && Main.tile[i, j - 1].liquid == 0 && Main.tile[i, j - 1].wall == 0 && Main.rand.Next(300) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<NightshadeTile>(), true, false, -1, 0);
			}
			if (type == 2 && Main.dayTime && RedeWorld.downedThorn && !Framing.GetTileSafely(i, j - 1).active() && !Framing.GetTileSafely(i, j - 2).active() && !Framing.GetTileSafely(i + 1, j - 1).active() && !Framing.GetTileSafely(i + 1, j - 2).active() && Main.tile[i, j].active() && Main.tile[i + 1, j].active() && Main.tile[i, j - 1].liquid == 0 && Main.tile[i, j - 1].wall == 0 && Main.rand.Next(6000) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<AnglonicMysticBlossomTile>(), true, false, -1, 0);
			}
			if ((type == ModContent.TileType<DeadGrassTileCorruption>() || type == ModContent.TileType<IrradiatedEbonstoneTile>() || type == ModContent.TileType<DeadGrassTileCrimson>() || type == ModContent.TileType<IrradiatedCrimstoneTile>()) && Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.tile[i, j - 1].liquid == 0 && Main.rand.Next(300) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<RadRootTile>(), true, false, -1, 0);
			}
		}

		public override bool Drop(int i, int j, int type)
		{
			Player player = Main.player[Main.myPlayer];
			if (Main.netMode != 1 && !WorldGen.noTileActions && !WorldGen.gen)
			{
				if (type == 5 && Main.tile[i, j + 1].type == 2 && Main.rand.Next(6) == 0)
				{
					Projectile.NewProjectile((float)(i * 16), (float)((j - 10) * 16), (float)(-4 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(-3, 0)), ModContent.ProjectileType<TreeBugFall>(), 0, 0f, 255, 0f, 0f);
				}
				if (type == 323 && Main.tile[i, j + 1].type == 53 && Main.rand.Next(6) == 0)
				{
					Projectile.NewProjectile((float)(i * 16), (float)((j - 10) * 16), (float)(-4 + Main.rand.Next(0, 7)), (float)(-3 + Main.rand.Next(-3, 0)), ModContent.ProjectileType<CoastScarabFall>(), 0, 0f, 255, 0f, 0f);
				}
			}
			return (type != ModContent.TileType<AncientDirtTile>() || !TileID.Sets.BreakableWhenPlacing[ModContent.TileType<AncientDirtTile>()]) && (type != ModContent.TileType<ShadestoneTile>() || !TileID.Sets.BreakableWhenPlacing[ModContent.TileType<ShadestoneTile>()]) && (type != ModContent.TileType<ShadestoneBrickTile>() || !TileID.Sets.BreakableWhenPlacing[ModContent.TileType<ShadestoneBrickTile>()]) && base.Drop(i, j, type);
		}
	}
}
