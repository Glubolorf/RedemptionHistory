using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class DeadGrassTileCorruption : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			base.SetModTree(new DeadTree());
			Main.tileMerge[(int)base.Type][base.mod.TileType("DeadGrassTile")] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("DeadGrassTileCorruption")] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("DeadGrassTileCrimson")] = true;
			TileID.Sets.Conversion.Grass[(int)base.Type] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			TileID.Sets.NeedsGrassFraming[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			base.AddMapEntry(new Color(140, 140, 140), null);
			this.drop = 2;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 15)
			{
				player.AddBuff(base.mod.BuffType("RadioactiveFalloutDebuff"), Main.rand.Next(10, 20), true);
			}
		}

		public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
		{
			TileObject toBePlaced;
			if (!TileObject.CanPlace(x, y, type, style, direction, ref toBePlaced, false, false))
			{
				return false;
			}
			toBePlaced.random = random;
			if (TileObject.Place(toBePlaced) && !mute)
			{
				WorldGen.SquareTileFrame(x, y, true);
			}
			return false;
		}

		public override void RandomUpdate(int i, int j)
		{
			if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(10) == 0)
			{
				switch (Main.rand.Next(5))
				{
				case 0:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA1"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA1"), 0, 0, -1, -1);
					break;
				case 1:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA2"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA2"), 0, 0, -1, -1);
					break;
				case 2:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA3"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA3"), 0, 0, -1, -1);
					break;
				case 3:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA4"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA4"), 0, 0, -1, -1);
					break;
				default:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA5"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA5"), 0, 0, -1, -1);
					break;
				}
			}
			if (Main.rand.Next(12) == 0)
			{
				this.XenomiteSpawn(i, j);
			}
		}

		private bool XenomiteSpawn(int i, int j)
		{
			if (Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.rand.Next(12) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("XenomiteCrystalTile"), true, false, -1, 0);
				return true;
			}
			return false;
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.08f;
			g = 0.02f;
			b = 0.1f;
		}

		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return base.mod.TileType("DeadSapling");
		}
	}
}
