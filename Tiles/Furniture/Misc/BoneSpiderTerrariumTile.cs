﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.Misc;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Misc
{
	public class BoneSpiderTerrariumTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.addTile((int)base.Type);
			this.disableSmartCursor = true;
			this.animationFrameHeight = 48;
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Bone Spider Cage");
			base.AddMapEntry(Color.Gold, name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<BoneSpiderTerrarium>(), 1, false, 0, false, false);
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (frame >= 0 && frame <= 3)
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
							frame = 4;
						}
					}
					else
					{
						frame++;
						if (frame == 3)
						{
							frame = 0;
						}
					}
					frameCounter = 0;
					return;
				}
			}
			else if (frame >= 4 && frame <= 8)
			{
				frameCounter++;
				if (frameCounter >= 10)
				{
					frameCounter = 0;
					frame++;
					if (frame == 8)
					{
						frame = 0;
						return;
					}
				}
			}
			else if (frame >= 9)
			{
				frameCounter++;
				if (frameCounter >= 10)
				{
					frameCounter = 0;
					frame++;
					if (frame >= 27)
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