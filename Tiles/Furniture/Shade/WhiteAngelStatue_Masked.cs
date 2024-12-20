using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class WhiteAngelStatue_Masked : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.Width = 9;
			TileObjectData.newTile.Height = 16;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.newTile.Origin = new Point16(4, 15);
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("White Angel Statue");
			base.AddMapEntry(new Color(255, 255, 255), name);
			this.minPick = 500;
			this.mineResist = 30f;
			this.soundType = 21;
			this.dustType = 261;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Texture2D beam = base.mod.GetTexture("ExtraTextures/WhiteLightBeam");
			Rectangle rect = new Rectangle(0, 0, beam.Width, beam.Height);
			Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			if (Main.tile[i, j].frameX == 16 && Main.tile[i, j].frameY == 240)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null);
				Main.spriteBatch.Draw(beam, new Vector2(((float)i - 3.75f) * 16f - (float)((int)Main.screenPosition.X), (float)((j - 24) * 16 - (int)Main.screenPosition.Y)) + zero, new Rectangle?(rect), RedeColor.COLOR_WHITEFADE3, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null);
			}
		}
	}
}
