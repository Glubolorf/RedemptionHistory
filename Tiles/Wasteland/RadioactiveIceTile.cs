using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class RadioactiveIceTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			TileID.Sets.Conversion.Ice[(int)base.Type] = true;
			TileID.Sets.Ices[(int)base.Type] = true;
			Main.tileMerge[147][(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			base.AddMapEntry(new Color(70, 130, 70), null);
			this.drop = base.mod.ItemType("RadioactiveIce");
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 15)
			{
				player.AddBuff(base.mod.BuffType("RadioactiveFalloutDebuff"), Main.rand.Next(10, 20), true);
			}
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 0.04f;
			b = 0f;
		}

		public override void RandomUpdate(int i, int j)
		{
			if (Main.rand.Next(8) == 0 && !this.XenomiteSpawn(i, j))
			{
				this.SpawnRocks(i, j);
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

		private bool SpawnRocks(int i, int j)
		{
			if (Main.tile[i, j + 1].type == 0 && Main.tile[i, j + 2].type == 0 && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j + 1, base.mod.TileType("RadioactiveIciclesTile"), true, false, -1, 0);
				return true;
			}
			if (Main.tile[i, j + 1].type == 0 && Main.rand.Next(4) == 0)
			{
				WorldGen.PlaceTile(i, j + 1, base.mod.TileType("RadioactiveIcicles2Tile"), true, false, -1, 0);
				return true;
			}
			return false;
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}

		public override bool CanExplode(int i, int j)
		{
			return true;
		}
	}
}
