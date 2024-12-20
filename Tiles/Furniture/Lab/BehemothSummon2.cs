using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class BehemothSummon2 : ModTile
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

		public override bool NewRightClick(int i, int j)
		{
			Player p = Main.LocalPlayer;
			if (!NPC.AnyNPCs(ModContent.NPCType<IrradiatedBehemoth2>()) && RedeWorld.downedJanitor && RedeWorld.downedStage3Scientist && (!RedeWorld.labAccess[2] || RedeWorld.downedPatientZero))
			{
				if (Main.netMode != 1)
				{
					if (Main.tile[i, j].frameX >= 18)
					{
						int index = NPC.NewNPC(i * 16, j * 16, ModContent.NPCType<IrradiatedBehemoth2>(), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[index].netUpdate2 = true;
					}
					else
					{
						int index2 = NPC.NewNPC((i + 1) * 16, j * 16, ModContent.NPCType<IrradiatedBehemoth2>(), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[index2].netUpdate2 = true;
					}
				}
				else
				{
					if (Main.netMode == 0)
					{
						return false;
					}
					if (Main.tile[i, j].frameX >= 18)
					{
						Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 0, new object[]
						{
							(byte)p.whoAmI,
							ModContent.NPCType<IrradiatedBehemoth2>(),
							"Irradiated Behemoth Has Been Summoned!",
							i * 16,
							j * 16
						}).Send(-1, -1);
					}
					else
					{
						Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 0, new object[]
						{
							(byte)p.whoAmI,
							ModContent.NPCType<IrradiatedBehemoth2>(),
							"Irradiated Behemoth Has Been Summoned!",
							(i + 1) * 16,
							j * 16
						}).Send(-1, -1);
					}
				}
			}
			return true;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
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
