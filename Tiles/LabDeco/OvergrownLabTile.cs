using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.LabDeco
{
	public class OvergrownLabTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("LabTileUnsafe")] = true;
			this.dustType = 226;
			this.drop = base.mod.ItemType("LabPlating");
			this.minPick = 500;
			this.mineResist = 3f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(200, 200, 200), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 15)
			{
				player.AddBuff(base.mod.BuffType("RadioactiveFalloutDebuff"), Main.rand.Next(10, 20), true);
				player.enemySpawns = false;
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
				switch (Main.rand.Next(7))
				{
				case 0:
					OvergrownLabTile.PlaceObject(i, j - 1, base.mod.TileType("LabShrub1"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("LabShrub1"), 0, 0, -1, -1);
					return;
				case 1:
					OvergrownLabTile.PlaceObject(i, j - 1, base.mod.TileType("LabShrub2"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("LabShrub2"), 0, 0, -1, -1);
					return;
				case 2:
					OvergrownLabTile.PlaceObject(i, j - 1, base.mod.TileType("LabShrub3"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("LabShrub3"), 0, 0, -1, -1);
					return;
				case 3:
					OvergrownLabTile.PlaceObject(i, j - 1, base.mod.TileType("LabShrub4"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("LabShrub4"), 0, 0, -1, -1);
					return;
				case 4:
					OvergrownLabTile.PlaceObject(i, j - 1, base.mod.TileType("LabShrub5"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("LabShrub5"), 0, 0, -1, -1);
					return;
				case 5:
					OvergrownLabTile.PlaceObject(i, j - 1, base.mod.TileType("LabShrub6"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("LabShrub6"), 0, 0, -1, -1);
					return;
				default:
					OvergrownLabTile.PlaceObject(i, j - 1, base.mod.TileType("LabShrub7"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, i, j - 1, base.mod.TileType("LabShrub7"), 0, 0, -1, -1);
					break;
				}
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return RedeWorld.downedPatientZero;
		}
	}
}
