using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Usable.Potions;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.NPCs.Soulless;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Natural
{
	public class ShadePots : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			Main.tileCut[(int)base.Type] = true;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.dustType = ModContent.DustType<VoidFlame>();
			this.soundType = 13;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(140, 140, 170), null);
			this.disableSmartCursor = true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), new Vector2((float)Main.rand.Next(-4, 5), (float)Main.rand.Next(-4, 5)), base.mod.GetGoreSlot("Gores/Misc/ShadePotGore1"), 1f);
			if (Utils.NextBool(Main.rand, 2))
			{
				Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), new Vector2((float)Main.rand.Next(-4, 5), (float)Main.rand.Next(-4, 5)), base.mod.GetGoreSlot("Gores/Misc/ShadePotGore2"), 1f);
			}
			if (Utils.NextBool(Main.rand, 2))
			{
				Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), new Vector2((float)Main.rand.Next(-4, 5), (float)Main.rand.Next(-4, 5)), base.mod.GetGoreSlot("Gores/Misc/ShadePotGore3"), 1f);
			}
			if (Utils.NextBool(Main.rand, 2))
			{
				Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), new Vector2((float)Main.rand.Next(-4, 5), (float)Main.rand.Next(-4, 5)), base.mod.GetGoreSlot("Gores/Misc/ShadePotGore4"), 1f);
			}
			if (Main.rand.Next(250) == 0)
			{
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(((float)i + 1.5f) * 16f, (float)j * 16f, 0f, 0f, 518, 0, 0f, Main.myPlayer, 0f, 0f);
					return;
				}
			}
			else if (Main.expertMode ? (Main.rand.Next(45) < 2) : (Main.rand.Next(45) == 0))
			{
				switch (Main.rand.Next(11))
				{
				case 0:
					Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<ElectrifiedPotion>(), 1, false, 0, false, false);
					return;
				case 1:
				case 2:
				case 3:
					break;
				case 4:
					Item.NewItem(i * 16, j * 16, 32, 16, 300, 1, false, 0, false, false);
					return;
				case 5:
					Item.NewItem(i * 16, j * 16, 32, 16, 2346, 1, false, 0, false, false);
					return;
				case 6:
					Item.NewItem(i * 16, j * 16, 32, 16, 297, 1, false, 0, false, false);
					return;
				case 7:
					Item.NewItem(i * 16, j * 16, 32, 16, 293, 1, false, 0, false, false);
					return;
				case 8:
					Item.NewItem(i * 16, j * 16, 32, 16, 294, 1, false, 0, false, false);
					return;
				case 9:
					Item.NewItem(i * 16, j * 16, 32, 16, 2326, 1, false, 0, false, false);
					return;
				case 10:
					Item.NewItem(i * 16, j * 16, 32, 16, 2349, 1, false, 0, false, false);
					return;
				default:
					return;
				}
			}
			else
			{
				switch (Main.rand.Next(8))
				{
				case 0:
					Item.NewItem(i * 16, j * 16, 32, 16, 58, 1, false, 0, false, false);
					if (Main.rand.Next(2) == 0)
					{
						Item.NewItem(i * 16, j * 16, 32, 16, 58, 1, false, 0, false, false);
						return;
					}
					break;
				case 1:
					if (Main.tile[i, j].liquid == 255 && Main.tile[i, j].liquidType() == 0)
					{
						Item.NewItem(i * 16, j * 16, 32, 16, 282, Main.rand.Next(Main.expertMode ? 5 : 4, Main.expertMode ? 18 : 12), false, 0, false, false);
						return;
					}
					Item.NewItem(i * 16, j * 16, 32, 16, 8, Main.rand.Next(Main.expertMode ? 5 : 4, Main.expertMode ? 18 : 12), false, 0, false, false);
					return;
				case 2:
					Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<ShadeKnife>(), Main.rand.Next(10, 20), false, 0, false, false);
					return;
				case 3:
					Item.NewItem(i * 16, j * 16, 32, 16, 3544, 1, false, 0, false, false);
					if (Main.rand.Next(3) == 0)
					{
						Item.NewItem(i * 16, j * 16, 32, 16, 3544, 1, false, 0, false, false);
						return;
					}
					break;
				case 4:
					Item.NewItem(i * 16, j * 16, 32, 16, 166, Main.rand.Next(1, Main.expertMode ? 7 : 4), false, 0, false, false);
					return;
				case 5:
				case 6:
					for (int k = 0; k < Main.rand.Next(1, 4); k++)
					{
						if (Main.rand.Next(2) == 0)
						{
							Item.NewItem(i * 16, j * 16, 32, 16, 71, Main.rand.Next(1, 99), false, 0, false, false);
						}
					}
					for (int l = 0; l < Main.rand.Next(1, 4); l++)
					{
						if (Main.rand.Next(2) == 0)
						{
							Item.NewItem(i * 16, j * 16, 32, 16, 72, Main.rand.Next(1, 50), false, 0, false, false);
						}
					}
					for (int m = 0; m < Main.rand.Next(1, 3); m++)
					{
						if (Main.rand.Next(4) == 0)
						{
							Item.NewItem(i * 16, j * 16, 32, 16, 73, Main.rand.Next(1, 3), false, 0, false, false);
						}
					}
					return;
				case 7:
				{
					int index = NPC.NewNPC((int)(((float)i + 1.5f) * 16f), (int)(((float)j + 1.5f) * 16f), ModContent.NPCType<LaughingMaskSmall>(), 0, 0f, 0f, 0f, 0f, 255);
					if (index < 200 && Main.netMode == 1)
					{
						NetMessage.SendData(23, -1, -1, null, index, 0f, 0f, 0f, 0, 0, 0);
					}
					break;
				}
				default:
					return;
				}
			}
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
	}
}
