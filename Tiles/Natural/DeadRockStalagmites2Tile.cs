using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Natural
{
	public class DeadRockStalagmites2Tile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				ModContent.TileType<DeadRockTile>(),
				ModContent.TileType<IrradiatedCrimstoneTile>(),
				ModContent.TileType<IrradiatedEbonstoneTile>()
			};
			TileObjectData.addTile((int)base.Type);
			this.disableSmartCursor = true;
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
			if (i % 6 < 3)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			frameXOffset = i % 3 * 18;
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 4;
		}
	}
}
