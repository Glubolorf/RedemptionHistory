using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Plants;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Plants
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
			this.disableSmartCursor = true;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Nightshade");
			base.AddMapEntry(Color.Purple, name);
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Origin = Point16.Zero;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(33, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.WaterPlacement = 1;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.LavaPlacement = 1;
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

		public override void NearbyEffects(int i, int j, bool closer)
		{
			int stage = (int)(Main.tile[i, j].frameX / 18);
			if (stage == 1 && !Main.dayTime)
			{
				Tile tile = Main.tile[i, j];
				tile.frameX += 18;
				return;
			}
			if (stage == 2 && Main.dayTime)
			{
				Tile tile2 = Main.tile[i, j];
				tile2.frameX -= 18;
			}
		}

		public override bool Drop(int i, int j)
		{
			short num = Main.tile[i, j].frameX / 18;
			if (num == 1 && Main.rand.Next(4) == 0)
			{
				Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<NightshadeSeeds>(), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<NightshadeSeeds>(), 1, false, 0, false, false);
				Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<Nightshade>(), 1, false, 0, false, false);
			}
			return false;
		}

		public override void RandomUpdate(int i, int j)
		{
			if (Main.tile[i, j].frameX == 0 && !Main.dayTime)
			{
				Tile tile = Main.tile[i, j];
				tile.frameX += 18;
			}
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			short num = Main.tile[i, j].frameX / 18;
			if (num == 2)
			{
				r = 0.2f;
				g = 0f;
				b = 0.3f;
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
