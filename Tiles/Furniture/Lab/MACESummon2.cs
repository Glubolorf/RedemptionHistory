using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class MACESummon2 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 2;
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

		public override bool NewRightClick(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			if (!NPC.AnyNPCs(ModContent.NPCType<MACEProjectHeadA>()) && !NPC.AnyNPCs(ModContent.NPCType<MACEProjectJawA>()) && RedeWorld.downedJanitor && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && RedeWorld.downedVolt && (!RedeWorld.labAccess[5] || RedeWorld.downedPatientZero))
			{
				RedeWorld.maceUS = true;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			return true;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player p = Main.LocalPlayer;
			if (!NPC.AnyNPCs(ModContent.NPCType<MACEProjectJawA>()) && !NPC.AnyNPCs(ModContent.NPCType<MACEProjectHeadA>()) && !NPC.AnyNPCs(ModContent.NPCType<MACEProjectOffA>()))
			{
				if (Main.netMode != 1)
				{
					int index = NPC.NewNPC((i + 15) * 16, (j - 7) * 16, ModContent.NPCType<MACEProjectOffA>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[index].netUpdate2 = true;
					return;
				}
				if (Main.netMode != 0)
				{
					Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 1, new object[]
					{
						(byte)p.whoAmI,
						ModContent.NPCType<MACEProjectOffA>(),
						(i + 15) * 16,
						(int)(((float)j - 12.5f) * 16f)
					}).Send(-1, -1);
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
