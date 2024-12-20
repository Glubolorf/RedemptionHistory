using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class StarliteGemOreTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("DeadRockTile")] = true;
			this.drop = base.mod.ItemType("Starlite");
			this.minPick = 180;
			this.mineResist = 3.5f;
			this.soundType = 21;
			base.AddMapEntry(new Color(30, 65, 25), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player localPlayer = Main.LocalPlayer;
			int num = (int)Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i, (float)j));
			if (num <= 15)
			{
				localPlayer.AddBuff(base.mod.BuffType("RadioactiveFalloutDebuff"), Main.rand.Next(10, 20), true);
			}
		}

		public override void RandomUpdate(int i, int j)
		{
			if (Main.rand.Next(8) == 0 && !this.StarliteGemSpawn(i, j))
			{
				bool flag = this.SpawnRocks(i, j);
			}
		}

		private bool StarliteGemSpawn(int i, int j)
		{
			if (Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("StarliteGemTile"), true, false, -1, 0);
				return true;
			}
			return false;
		}

		private bool SpawnRocks(int i, int j)
		{
			if (Main.tile[i, j - 1].type == 0 && Main.tile[i, j - 2].type == 0 && Main.rand.Next(6) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("DeadRockStalagmitesTile"), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j + 1].type == 0 && Main.tile[i, j + 2].type == 0 && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j + 1, base.mod.TileType("DeadRockStalacmitesTile"), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j - 1].type == 0 && Main.rand.Next(6) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("DeadRockStalagmites2Tile"), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j + 1].type == 0 && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j + 1, base.mod.TileType("DeadRockStalacmites2Tile"), true, false, -1, 0);
				return true;
			}
			return false;
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}
	}
}
