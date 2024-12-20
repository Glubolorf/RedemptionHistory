using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Placeable.Tiles;
using Redemption.Tiles.Natural;
using Redemption.Tiles.Ores;
using Redemption.Tiles.Trees;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class IrradiatedCrimstoneTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<StarliteGemOreTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<DeadRockTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<IrradiatedEbonstoneTile>()] = true;
			this.drop = ModContent.ItemType<IrradiatedCrimstone>();
			TileID.Sets.Conversion.Stone[(int)base.Type] = true;
			this.dustType = 31;
			this.minPick = 180;
			this.mineResist = 2.5f;
			this.soundType = 21;
			base.AddMapEntry(new Color(30, 50, 25), null);
			base.SetModTree(new DeadTree());
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
			if (Main.rand.Next(8) == 0)
			{
				bool spawned = this.StarliteGemSpawn(i, j);
				if (!spawned)
				{
					spawned = this.SpawnRocks(i, j);
				}
				if (!spawned)
				{
					spawned = this.SpawnBigXeno(i, j);
				}
			}
		}

		private bool SpawnBigXeno(int i, int j)
		{
			if (Main.tile[i, j - 1].type == 0 && Main.tile[i + 1, j - 1].type == 0 && Main.tile[i, j - 2].type == 0 && Main.tile[i + 1, j - 2].type == 0 && Main.tile[i + 3, j].type == 0 && Main.tile[i + 3, j - 1].type == 0 && Main.tile[i + 3, j - 2].type == 0 && Main.tile[i + 3, j - 3].type == 0 && Main.rand.Next(50) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<XenomiteCrystalBigTile>(), true, false, -1, 0);
				return true;
			}
			return false;
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

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.04f;
			g = 0.01f;
			b = 0f;
		}

		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return ModContent.TileType<DeadSapling>();
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}
	}
}
