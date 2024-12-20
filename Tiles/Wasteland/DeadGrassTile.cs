using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class DeadGrassTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			base.SetModTree(new DeadTree());
			Main.tileMerge[(int)base.Type][base.mod.TileType("DeadGrassTile")] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			base.AddMapEntry(new Color(140, 140, 140), null);
			this.drop = 2;
		}

		public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
		{
			TileObject tileObject;
			if (!TileObject.CanPlace(x, y, type, style, direction, ref tileObject, false, false))
			{
				return false;
			}
			tileObject.random = random;
			if (TileObject.Place(tileObject) && !mute)
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
					return;
				case 1:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA2"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA2"), 0, 0, -1, -1);
					return;
				case 2:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA3"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA3"), 0, 0, -1, -1);
					return;
				case 3:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA4"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA4"), 0, 0, -1, -1);
					return;
				default:
					DeadGrassTile.PlaceObject(i, j - 1, base.mod.TileType("DeadGrassA5"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("DeadGrassA5"), 0, 0, -1, -1);
					break;
				}
			}
		}

		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return base.mod.TileType("DeadSapling");
		}
	}
}
