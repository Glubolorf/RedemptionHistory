using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class PatientZeroSummon : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
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
			base.AddMapEntry(new Color(70, 120, 150), null);
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override bool NewRightClick(int i, int j)
		{
			if (Main.netMode == 0)
			{
				if (Main.tile[i, j].frameY >= 36)
				{
					if (!NPC.AnyNPCs(base.mod.NPCType("PatientZero")) && RedeWorld.downedStage2Scientist && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && RedeWorld.downedMACE)
					{
						Player localPlayer = Main.LocalPlayer;
						Main.tile[i, j];
						i *= 16;
						j--;
						j *= 16;
						int k = NPC.NewNPC(i, j, base.mod.NPCType("PatientZero"), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
				else if (Main.tile[i, j].frameY >= 18)
				{
					if (!NPC.AnyNPCs(base.mod.NPCType("PatientZero")) && RedeWorld.downedStage2Scientist && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && RedeWorld.downedMACE)
					{
						Player localPlayer2 = Main.LocalPlayer;
						Main.tile[i, j];
						i *= 16;
						j *= 16;
						int l = NPC.NewNPC(i, j, base.mod.NPCType("PatientZero"), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, l, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
				else if (!NPC.AnyNPCs(base.mod.NPCType("PatientZero")) && RedeWorld.downedStage2Scientist && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && RedeWorld.downedMACE)
				{
					Player localPlayer3 = Main.LocalPlayer;
					Main.tile[i, j];
					i *= 16;
					j++;
					j *= 16;
					int m = NPC.NewNPC(i, j, base.mod.NPCType("PatientZero"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, m, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			else if (Main.tile[i, j].frameY >= 36)
			{
				if (!NPC.AnyNPCs(base.mod.NPCType("PatientZero")) && RedeWorld.downedStage2Scientist && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && RedeWorld.downedMACE)
				{
					ModPacket packet = base.mod.GetPacket(256);
					packet.Write(12);
					Utils.WriteVector2(packet, new Vector2((float)(i * 16), (float)(j * 16 - 1)));
					packet.Send(-1, -1);
				}
			}
			else if (Main.tile[i, j].frameY >= 18)
			{
				if (!NPC.AnyNPCs(base.mod.NPCType("PatientZero")) && RedeWorld.downedStage2Scientist && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && RedeWorld.downedMACE)
				{
					ModPacket packet2 = base.mod.GetPacket(256);
					packet2.Write(12);
					Utils.WriteVector2(packet2, new Vector2((float)(i * 16), (float)(j * 16)));
					packet2.Send(-1, -1);
				}
			}
			else if (!NPC.AnyNPCs(base.mod.NPCType("PatientZero")) && RedeWorld.downedStage2Scientist && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && RedeWorld.downedMACE)
			{
				ModPacket packet3 = base.mod.GetPacket(256);
				packet3.Write(12);
				Utils.WriteVector2(packet3, new Vector2((float)(i * 16), (float)(j * 16 + 1)));
				packet3.Send(-1, -1);
			}
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = base.mod.ItemType("SignDeath");
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
