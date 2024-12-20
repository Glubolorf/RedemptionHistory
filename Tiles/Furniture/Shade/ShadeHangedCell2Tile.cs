using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.NPCs.Soulless;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeHangedCell2Tile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = false;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.AnchorTop = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Hanged Cage");
			base.AddMapEntry(new Color(45, 45, 45), name);
			this.dustType = ModContent.DustType<VoidFlame>();
			this.animationFrameHeight = 72;
			this.minPick = 310;
			this.mineResist = 11f;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (frame == 0)
			{
				if (frameCounter > Main.rand.Next(120, 300))
				{
					if (Main.rand.Next(3) != 0)
					{
						frame = 1;
					}
					frameCounter = 0;
					return;
				}
			}
			else if (frame >= 1 && frame <= 5)
			{
				frameCounter++;
				if (frameCounter >= 10)
				{
					frameCounter = 0;
					frame++;
					return;
				}
			}
			else if (frame >= 6)
			{
				if (frameCounter > Main.rand.Next(100, 200))
				{
					if (Main.rand.Next(3) != 0)
					{
						frame = 7;
					}
					frameCounter = 0;
					return;
				}
			}
			else if (frame >= 7 && frame <= 8)
			{
				frameCounter++;
				if (frameCounter >= 10)
				{
					frameCounter = 0;
					frame++;
					if (frame >= 9)
					{
						frame = 0;
					}
				}
			}
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int index = NPC.NewNPC((int)(((float)i + 1.5f) * 16f), (int)(((float)j + 3.5f) * 16f), ModContent.NPCType<Echo>(), 0, 0f, 0f, 0f, 0f, 255);
			if (index < 200 && Main.netMode == 1)
			{
				NetMessage.SendData(23, -1, -1, null, index, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}
	}
}
