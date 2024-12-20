using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.Lab;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class LabDoorBrokenTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 7;
			this.minPick = 500;
			this.mineResist = 3f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(180, 180, 185), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (Main.netMode != 1)
			{
				Player player = Main.LocalPlayer;
				float dist = Vector2.Distance(player.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
				if (dist <= 12f && dist > 6f)
				{
					if (Main.rand.Next(350) == 0 && NPC.CountNPCS(ModContent.NPCType<WalterInfected>()) <= 3)
					{
						if (Main.netMode != 1)
						{
							int index = NPC.NewNPC((i + 1) * 16, (j + 1) * 16, ModContent.NPCType<WalterInfected>(), 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[index].netUpdate2 = true;
						}
						else if (Main.netMode != 0)
						{
							Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 1, new object[]
							{
								(byte)player.whoAmI,
								ModContent.NPCType<XenoChomper2>(),
								(i + 1) * 16,
								(j + 1) * 16
							}).Send(-1, -1);
						}
					}
					if (NPC.downedMoonlord && Main.rand.Next(600) == 0 && NPC.CountNPCS(ModContent.NPCType<Stage2Scientist>()) == 0)
					{
						if (Main.netMode != 1)
						{
							int index2 = NPC.NewNPC((i + 1) * 16, (j + 1) * 16, ModContent.NPCType<Stage2Scientist>(), 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[index2].netUpdate2 = true;
							return;
						}
						if (Main.netMode != 0)
						{
							Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 2, new object[]
							{
								(byte)player.whoAmI,
								ModContent.NPCType<Stage2Scientist>(),
								(i + 1) * 16,
								(j + 1) * 16
							}).Send(-1, -1);
						}
					}
				}
			}
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<LabDoor2>(), 1, false, 0, false, false);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
