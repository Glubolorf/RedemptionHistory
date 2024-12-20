using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items.Placeable.Tiles;
using Redemption.Tiles.Plants;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class ShadestoneMossyTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneRubbleTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneHSlabTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickMossyTile>()] = true;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.drop = ModContent.ItemType<Shadestone>();
			this.minPick = 350;
			this.mineResist = 11f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Mossy Shadestone");
			base.AddMapEntry(new Color(10, 10, 10), null);
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			if (Utils.NextBool(Main.rand, 40000) && Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneSoulless)
			{
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 0, 0, ModContent.DustType<SoullessScreenDust>(), 0f, 0f, 0, default(Color), 1f);
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

		public override void RandomUpdate(int i, int j)
		{
			if (!Framing.GetTileSafely(i, j + 1).active() && Main.tile[i, j].active() && Main.rand.Next(5) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Nooseroot_Small>(), true, Main.rand.Next(3), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Nooseroot_Small>(), Main.rand.Next(3), 0, -1, -1);
			}
			if (!Framing.GetTileSafely(i, j + 1).active() && Main.tile[i, j].active() && Main.rand.Next(7) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Nooseroot_Medium>(), true, Main.rand.Next(3), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Nooseroot_Medium>(), Main.rand.Next(3), 0, -1, -1);
			}
			if (!Framing.GetTileSafely(i, j + 1).active() && Main.tile[i, j].active() && Main.rand.Next(12) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<Nooseroot_Large>(), true, Main.rand.Next(3), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<Nooseroot_Large>(), Main.rand.Next(3), 0, -1, -1);
			}
			if (Utils.NextBool(Main.rand, 10))
			{
				WorldGen.SpreadGrass(i + Main.rand.Next(-1, 1), j + Main.rand.Next(-1, 1), ModContent.TileType<ShadestoneTile>(), ModContent.TileType<ShadestoneMossyTile>(), false, 0);
			}
		}
	}
}
