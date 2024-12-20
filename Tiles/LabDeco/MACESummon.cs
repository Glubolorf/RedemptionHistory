using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class MACESummon : ModTile
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

		public override void RightClick(int i, int j)
		{
			if (Main.netMode != 1)
			{
				if (Main.tile[i, j].frameY >= 18)
				{
					if (!NPC.AnyNPCs(base.mod.NPCType("MACEProjectJaw")) && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectHead")) && RedeWorld.downedStage2Scientist && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && !RedeWorld.labAccess6)
					{
						Player localPlayer = Main.LocalPlayer;
						Main.tile[i, j];
						i *= 16;
						j *= 16;
						int num = NPC.NewNPC(i, j, base.mod.NPCType("MACEProjectHead"), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
					}
				}
				else if (!NPC.AnyNPCs(base.mod.NPCType("MACEProjectJaw")) && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectHead")) && RedeWorld.downedStage2Scientist && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface && !RedeWorld.labAccess6)
				{
					Player localPlayer2 = Main.LocalPlayer;
					Main.tile[i, j];
					i *= 16;
					j++;
					j *= 16;
					int num2 = NPC.NewNPC(i, j, base.mod.NPCType("MACEProjectHead"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
					}
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
			localPlayer.showItemIcon2 = base.mod.ItemType("SignDeath");
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
