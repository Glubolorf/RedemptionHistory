using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class HalogenLampTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileLighted[(int)base.Type] = true;
			this.dustType = 226;
			this.minPick = 500;
			this.mineResist = 3f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(200, 200, 255), null);
			this.animationFrameHeight = 90;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void HitWire(int i, int j)
		{
			if (Main.tile[i, j].frameY >= 90)
			{
				Tile tile = Main.tile[i, j];
				tile.frameY -= 90;
				return;
			}
			Tile tile2 = Main.tile[i, j];
			tile2.frameY += 90;
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
			Main.spriteBatch.Draw(base.mod.GetTexture("Tiles/Tiles/HalogenLampTile_Glow"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X), (float)(j * 16 - (int)Main.screenPosition.Y)) + zero, new Rectangle?(new Rectangle((int)tile.frameX, (int)tile.frameY, 16, height)), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].frameY < 90)
			{
				r = 0f;
				g = 0.3f;
				b = 0.3f;
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
