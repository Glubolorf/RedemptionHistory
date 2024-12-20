﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class LabWideConsoleVolt : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
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
			base.AddMapEntry(new Color(70, 70, 70), null);
			this.animationFrameHeight = 36;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player localPlayer = Main.LocalPlayer;
			RedePlayer redePlayer = (RedePlayer)localPlayer.GetModPlayer(base.mod, "RedePlayer");
			if ((int)Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i, (float)j)) <= 100)
			{
				if (Main.netMode == 0)
				{
					if (!NPC.AnyNPCs(base.mod.NPCType("TbotMinibossStart")) && !NPC.AnyNPCs(base.mod.NPCType("TbotMiniboss")) && !NPC.AnyNPCs(base.mod.NPCType("ProtectorVoltNPC")) && RedeWorld.downedJanitor && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface)
					{
						Main.tile[i, j];
						i -= 36;
						i *= 16;
						j *= 16;
						if (RedeWorld.downedVolt)
						{
							int k = NPC.NewNPC(i, j, base.mod.NPCType("ProtectorVoltNPC"), 0, 0f, 0f, 0f, 0f, 255);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
								return;
							}
						}
						else
						{
							int l = NPC.NewNPC(i, j, base.mod.NPCType("TbotMinibossStart"), 0, 0f, 0f, 0f, 0f, 255);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(23, -1, -1, null, l, 0f, 0f, 0f, 0, 0, 0);
								return;
							}
						}
					}
				}
				else if (!NPC.AnyNPCs(base.mod.NPCType("TbotMinibossStart")) && !NPC.AnyNPCs(base.mod.NPCType("TbotMiniboss")) && !NPC.AnyNPCs(base.mod.NPCType("ProtectorVoltNPC")) && RedeWorld.downedJanitor && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface)
				{
					if (RedeWorld.downedVolt)
					{
						ModPacket packet = base.mod.GetPacket(256);
						packet.Write(18);
						Utils.WriteVector2(packet, new Vector2((float)(i * 16 - 36), (float)(j * 16)));
						packet.Send(-1, -1);
						return;
					}
					ModPacket packet2 = base.mod.GetPacket(256);
					packet2.Write(8);
					Utils.WriteVector2(packet2, new Vector2((float)(i * 16 - 36), (float)(j * 16)));
					packet2.Send(-1, -1);
				}
			}
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 4)
			{
				frameCounter = 0;
				frame++;
				if (frame > 1)
				{
					frame = 0;
				}
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
