using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.Misc;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Misc
{
	public class SandskinTerrariumTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.addTile((int)base.Type);
			this.disableSmartCursor = true;
			this.animationFrameHeight = 54;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Sandskin Spider Cage");
			base.AddMapEntry(Color.Gold, name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<SandskinTerrarium>(), 1, false, 0, false, false);
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (frame == 0)
			{
				frameCounter++;
				if (frameCounter > Main.rand.Next(180, 1800))
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
			else if (frame >= 6 && frame <= 8)
			{
				frameCounter++;
				if (frameCounter >= 10)
				{
					if (Main.rand.Next(30) == 0)
					{
						int num = Main.rand.Next(2);
						if (num != 0)
						{
							if (num == 1)
							{
								frame = 9;
							}
						}
						else
						{
							frame = 19;
						}
					}
					else
					{
						frame++;
						if (frame == 8)
						{
							frame = 6;
						}
					}
					frameCounter = 0;
					return;
				}
			}
			else if (frame >= 9 && frame <= 14)
			{
				frameCounter++;
				if (frameCounter >= 10)
				{
					frameCounter = 0;
					frame++;
					return;
				}
			}
			else if (frame == 15)
			{
				frameCounter++;
				if (frameCounter > Main.rand.Next(30, 900))
				{
					if (Main.rand.Next(3) != 0)
					{
						frame = 16;
					}
					frameCounter = 0;
					return;
				}
			}
			else if (frame >= 16 && frame <= 18)
			{
				frameCounter++;
				if (frameCounter >= 10)
				{
					frameCounter = 0;
					frame++;
					return;
				}
			}
			else if (frame >= 19)
			{
				frameCounter++;
				if (frameCounter >= 10)
				{
					frameCounter = 0;
					if (frame >= 26)
					{
						frame = 0;
						return;
					}
					frame++;
				}
			}
		}
	}
}
