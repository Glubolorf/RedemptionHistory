using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class PatientZeroSummon2 : ModTile
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

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player p = Main.LocalPlayer;
			if (!RedeWorld.downedPatientZero && !NPC.AnyNPCs(ModContent.NPCType<PZ2Eyelid>()) && !NPC.AnyNPCs(ModContent.NPCType<PZ2Fight>()))
			{
				if (Main.netMode != 1)
				{
					int index = NPC.NewNPC(i * 16, (j - 16) * 16, ModContent.NPCType<PZ2Eyelid>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[index].netUpdate2 = true;
					return;
				}
				Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 1, new object[]
				{
					(byte)p.whoAmI,
					ModContent.NPCType<PZ2Eyelid>(),
					i * 16,
					j * 16
				}).Send(-1, -1);
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			Player p = Main.LocalPlayer;
			if (!NPC.AnyNPCs(ModContent.NPCType<PZ2Fight>()) && RedeWorld.downedJanitor && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && RedeWorld.downedVolt && RedeWorld.downedMACE)
			{
				if (RedeWorld.downedPatientZero)
				{
					if (Main.netMode != 1)
					{
						if (Main.tile[i, j].frameY >= 36)
						{
							int index = NPC.NewNPC(i * 16, (j - 18) * 16, ModContent.NPCType<PZ2Fight>(), 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[index].netUpdate2 = true;
						}
						else if (Main.tile[i, j].frameY >= 18)
						{
							int index2 = NPC.NewNPC(i * 16, (j - 17) * 16, ModContent.NPCType<PZ2Fight>(), 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[index2].netUpdate2 = true;
						}
						else
						{
							int index3 = NPC.NewNPC(i * 16, (j - 16) * 16, ModContent.NPCType<PZ2Fight>(), 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[index3].netUpdate2 = true;
						}
					}
					else
					{
						if (Main.netMode == 0)
						{
							return false;
						}
						if (Main.tile[i, j].frameY >= 36)
						{
							Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 0, new object[]
							{
								(byte)p.whoAmI,
								ModContent.NPCType<PZ2Fight>(),
								"Patient Zero Has Been Summoned!",
								(i + 1) * 16,
								(j - 2) * 16
							}).Send(-1, -1);
						}
						else if (Main.tile[i, j].frameY >= 18)
						{
							Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 0, new object[]
							{
								(byte)p.whoAmI,
								ModContent.NPCType<PZ2Fight>(),
								"Patient Zero Has Been Summoned!",
								(i + 1) * 16,
								(j - 1) * 16
							}).Send(-1, -1);
						}
						else
						{
							Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 0, new object[]
							{
								(byte)p.whoAmI,
								ModContent.NPCType<PZ2Fight>(),
								"Patient Zero Has Been Summoned!",
								(i + 1) * 16,
								j * 16
							}).Send(-1, -1);
						}
					}
				}
				else
				{
					RedeWorld.pzUS = true;
					if (Main.netMode != 0)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<LabHelpMessage>();
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
