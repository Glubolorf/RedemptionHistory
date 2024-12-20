using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
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
			Player p = Main.LocalPlayer;
			if (!NPC.AnyNPCs(ModContent.NPCType<TbotMinibossStart>()) && !NPC.AnyNPCs(ModContent.NPCType<TbotMiniboss>()) && !NPC.AnyNPCs(ModContent.NPCType<ProtectorVoltNPC>()) && RedeWorld.downedJanitor && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface)
			{
				if (Main.netMode != 1)
				{
					if (RedeWorld.downedVolt)
					{
						int index = NPC.NewNPC((i - 36) * 16, j * 16, ModContent.NPCType<ProtectorVoltNPC>(), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[index].netUpdate2 = true;
						return;
					}
					int index2 = NPC.NewNPC((i - 36) * 16, j * 16, ModContent.NPCType<TbotMinibossStart>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[index2].netUpdate2 = true;
					return;
				}
				else if (Main.netMode != 0)
				{
					if (RedeWorld.downedVolt)
					{
						Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 1, new object[]
						{
							(byte)p.whoAmI,
							ModContent.NPCType<ProtectorVoltNPC>(),
							(i - 36) * 16,
							j * 16
						}).Send(-1, -1);
						return;
					}
					Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 1, new object[]
					{
						(byte)p.whoAmI,
						ModContent.NPCType<TbotMinibossStart>(),
						(i - 36) * 16,
						j * 16
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

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
