using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items;
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
			Main.tileValue[(int)base.Type] = 600;
			Main.tileMerge[(int)base.Type][ModContent.TileType<DeadRockTile>()] = true;
			this.drop = ModContent.ItemType<Starlite>();
			this.minPick = 180;
			this.mineResist = 3.5f;
			this.soundType = 21;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Starlite");
			base.AddMapEntry(new Color(30, 65, 25), name);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 15)
			{
				player.AddBuff(ModContent.BuffType<RadioactiveFalloutDebuff>(), Main.rand.Next(10, 20), true);
			}
		}

		public override void RandomUpdate(int i, int j)
		{
			if (Main.rand.Next(8) == 0 && !this.StarliteGemSpawn(i, j))
			{
				this.SpawnRocks(i, j);
			}
		}

		private bool StarliteGemSpawn(int i, int j)
		{
			if (Main.tile[i, j - 1].type == 0 && Main.tile[i, j].active() && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<StarliteGemTile>(), true, false, -1, 0);
				return true;
			}
			return false;
		}

		private bool SpawnRocks(int i, int j)
		{
			if (Main.tile[i, j - 1].type == 0 && Main.tile[i, j - 2].type == 0 && Main.rand.Next(6) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<DeadRockStalagmitesTile>(), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j + 1].type == 0 && Main.tile[i, j + 2].type == 0 && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j + 1, ModContent.TileType<DeadRockStalacmitesTile>(), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j - 1].type == 0 && Main.rand.Next(6) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<DeadRockStalagmites2Tile>(), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j + 1].type == 0 && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j + 1, ModContent.TileType<DeadRockStalacmites2Tile>(), true, false, -1, 0);
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
