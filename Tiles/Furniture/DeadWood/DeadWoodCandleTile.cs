using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Furniture.DeadWood;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.DeadWood
{
	public class DeadWoodCandleTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				20
			};
			TileObjectData.newTile.DrawYOffset = -4;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Petrified Wood Candle");
			base.AddMapEntry(new Color(200, 200, 200), name);
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				33
			};
			this.drop = ModContent.ItemType<DeadWoodCandle>();
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		}

		public override void HitWire(int i, int j)
		{
			if (Main.tile[i, j].frameX >= 18)
			{
				Tile tile = Main.tile[i, j];
				tile.frameX -= 18;
				return;
			}
			Tile tile2 = Main.tile[i, j];
			tile2.frameX += 18;
		}

		public override bool NewRightClick(int i, int j)
		{
			Main.player[Main.myPlayer].PickTile(i, j, 100);
			return true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<DeadWoodCandle>();
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].frameX < 18)
			{
				r = 0.7f;
				g = 0.7f;
				b = 0.7f;
			}
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
			Main.spriteBatch.Draw(base.mod.GetTexture("Tiles/Furniture/DeadWood/DeadWoodCandleTile_Glow"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X), (float)(j * 16 - (int)Main.screenPosition.Y)) + zero, new Rectangle?(new Rectangle((int)tile.frameX, (int)tile.frameY, 16, height)), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}
