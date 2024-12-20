using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class NightshadeTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileCut[(int)base.Type] = true;
			Main.tileNoFail[(int)base.Type] = true;
			Main.tileWaterDeath[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			this.dustType = 3;
			this.soundType = 6;
			Main.tileLighted[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				2,
				109
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
			short num = Main.tile[i, j].frameX / 18;
			if (num >= 1 && Main.rand.Next(4) == 0)
			{
				Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<NightshadeSeeds>(), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<Nightshade>(), 1, false, 0, false, false);
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
			if (Main.tile[i, j].frameX == 18 && !Main.dayTime)
			{
				Tile tile2 = Main.tile[i, j];
				tile2.frameX += 18;
				return;
			}
			if (Main.tile[i, j].frameX == 36 && Main.dayTime)
			{
				Tile tile3 = Main.tile[i, j];
				tile3.frameX -= 18;
			}
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			short num = Main.tile[i, j].frameX / 18;
			if (num == 2)
			{
				r = 0.1f;
				g = 0f;
				b = 0.2f;
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
