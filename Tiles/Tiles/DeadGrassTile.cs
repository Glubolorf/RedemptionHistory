using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Tiles.Plants;
using Redemption.Tiles.Trees;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class DeadGrassTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			base.SetModTree(new DeadTree());
			Main.tileMerge[(int)base.Type][ModContent.TileType<DeadGrassTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<DeadGrassTileCorruption>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<DeadGrassTileCrimson>()] = true;
			TileID.Sets.Conversion.Grass[(int)base.Type] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			TileID.Sets.NeedsGrassFraming[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			base.AddMapEntry(new Color(140, 140, 140), null);
			this.drop = 2;
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
			if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(10) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<DeadGrassA>(), true, Main.rand.Next(5), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<DeadGrassA>(), Main.rand.Next(5), 0, -1, -1);
			}
			WorldGen.SpreadGrass(i + Main.rand.Next(-1, 1), j + Main.rand.Next(-1, 1), 0, ModContent.TileType<DeadGrassTile>(), false, 0);
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

		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return ModContent.TileType<DeadSapling>();
		}
	}
}
