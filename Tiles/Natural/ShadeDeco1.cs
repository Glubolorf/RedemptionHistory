using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Natural
{
	public class ShadeDeco1 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			this.dustType = ModContent.DustType<VoidFlame>();
			TileObjectData.addTile((int)base.Type);
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(30, 30, 30), null);
			this.disableSmartCursor = true;
			this.animationFrameHeight = 36;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 4)
			{
				frameCounter = 0;
				frame++;
				if (frame > 3)
				{
					frame = 0;
				}
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
	}
}
