using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class XenoForgeTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Xeno Forge");
			base.AddMapEntry(new Color(200, 200, 200), name);
			this.disableSmartCursor = true;
			TileObjectData.addTile((int)base.Type);
			this.animationFrameHeight = 38;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 10)
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

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<XenoForge>(), 1, false, 0, false, false);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.2f;
			g = 0.6f;
			b = 0.2f;
		}
	}
}
