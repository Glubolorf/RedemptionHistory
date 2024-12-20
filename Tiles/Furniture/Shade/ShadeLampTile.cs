using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items.Placeable.Furniture.Shade;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeLampTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileWaterDeath[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(0, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
			TileObjectData.newTile.WaterDeath = true;
			TileObjectData.newTile.WaterPlacement = 1;
			TileObjectData.newTile.LavaPlacement = 1;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Shade Lamp");
			base.AddMapEntry(new Color(90, 90, 90), name);
			this.dustType = ModContent.DustType<VoidFlame>();
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			this.animationFrameHeight = 54;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 5)
			{
				frameCounter = 0;
				frame++;
				if (frame > 2)
				{
					frame = 0;
				}
			}
		}

		public override void HitWire(int i, int j)
		{
			int left = i - (int)(Main.tile[i, j].frameX / 18 % 1);
			int top = j - (int)(Main.tile[i, j].frameY / 18 % 3);
			for (int x = left; x < left + 1; x++)
			{
				for (int y = top; y < top + 3; y++)
				{
					if (Main.tile[x, y].frameX >= 18)
					{
						Tile tile = Main.tile[x, y];
						tile.frameX -= 18;
					}
					else
					{
						Tile tile2 = Main.tile[x, y];
						tile2.frameX += 18;
					}
				}
			}
			if (Wiring.running)
			{
				Wiring.SkipWire(left, top);
				Wiring.SkipWire(left, top + 1);
				Wiring.SkipWire(left, top + 2);
			}
			NetMessage.SendTileSquare(-1, left, top + 1, 2, 0);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].frameX < 18)
			{
				r = 0.7f;
				g = 0.7f;
				b = 0.8f;
			}
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<ShadeLamp>(), 1, false, 0, false, false);
			Chest.DestroyChest(i, j);
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int height = (tile.frameY == 36) ? 18 : 16;
			Main.spriteBatch.Draw(base.mod.GetTexture("Tiles/Furniture/Shade/ShadeLampTile_Glow"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X), (float)(j * 16 - (int)Main.screenPosition.Y)) + zero, new Rectangle?(new Rectangle((int)tile.frameX, (int)tile.frameY, 16, height)), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}
