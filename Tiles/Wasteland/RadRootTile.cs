using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Wasteland
{
	public class RadRootTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileCut[(int)base.Type] = true;
			Main.tileNoFail[(int)base.Type] = true;
			this.dustType = 273;
			this.soundType = 6;
			Main.tileLighted[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				ModContent.TileType<DeadGrassTileCorruption>(),
				ModContent.TileType<DeadGrassTileCrimson>(),
				ModContent.TileType<IrradiatedEbonstoneTile>(),
				ModContent.TileType<IrradiatedCrimstoneTile>()
			};
			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				78,
				380
			};
			TileObjectData.addTile((int)base.Type);
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
			if (i % 2 == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
		}

		public override bool Drop(int i, int j)
		{
			if (Main.tile[i, j].frameX / 18 == 2)
			{
				Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<RadRoot>(), 1, false, 0, false, false);
			}
			return false;
		}

		public override void RandomUpdate(int i, int j)
		{
			if (Main.tile[i, j].frameX == 0)
			{
				Tile tile = Main.tile[i, j];
				tile.frameX += 18;
				return;
			}
			if (Main.tile[i, j].frameX == 18)
			{
				Tile tile2 = Main.tile[i, j];
				tile2.frameX += 18;
			}
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			short num = Main.tile[i, j].frameX / 18;
			if (num == 2)
			{
				r = 0f;
				g = 0.2f;
				b = 0f;
			}
			if (num < 2)
			{
				r = 0f;
				g = 0f;
				b = 0f;
			}
		}
	}
}
