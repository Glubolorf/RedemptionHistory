using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class RadioactiveSandstoneTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("RadioactiveSandTile")] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("HardenedRadioactiveSandTile")] = true;
			TileID.Sets.Conversion.Sandstone[(int)base.Type] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			base.AddMapEntry(new Color(50, 70, 40), null);
			this.mineResist = 2.5f;
			this.drop = base.mod.ItemType("RadioactiveSandstone");
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 15)
			{
				player.AddBuff(base.mod.BuffType("RadioactiveFalloutDebuff"), Main.rand.Next(10, 20), true);
			}
		}

		public override void RandomUpdate(int i, int j)
		{
			if (Main.rand.Next(8) == 0)
			{
				bool spawned = this.StarliteGemSpawn(i, j);
				if (!spawned)
				{
					spawned = this.SpawnRocks(i, j);
				}
				if (!spawned)
				{
					spawned = this.SpawnNest(i, j);
				}
			}
		}

		private bool SpawnNest(int i, int j)
		{
			if (Main.tile[i, j - 1].type == 0 && Main.tile[i + 1, j - 1].type == 0 && Main.tile[i, j - 2].type == 0 && Main.tile[i + 1, j - 2].type == 0 && Main.rand.Next(12) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("GrubNestTile"), true, false, -1, 0);
				return true;
			}
			return false;
		}

		private bool StarliteGemSpawn(int i, int j)
		{
			if (Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.rand.Next(8) == 0)
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
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("RadioactiveSandstoneStalacmitesTile"), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j + 1].type == 0 && Main.tile[i, j + 2].type == 0 && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j + 1, base.mod.TileType("DeadRockStalacmitesTile"), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j - 1].type == 0 && Main.rand.Next(6) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, base.mod.TileType("RadioactiveSandstoneStalagmitesTile"), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j + 1].type == 0 && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j + 1, base.mod.TileType("RadioactiveSandstoneStalagmites2Tile"), true, false, -1, 0);
				return true;
			}
			return false;
		}

		public override bool CanExplode(int i, int j)
		{
			return true;
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}
	}
}
