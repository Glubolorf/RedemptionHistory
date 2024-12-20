using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.LabDeco;
using Redemption.NPCs.LabNPCs.New;
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
			if ((int)Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i, (float)j)) <= 100 && Main.netMode == 0 && !NPC.AnyNPCs(ModContent.NPCType<TbotMinibossStart>()) && !NPC.AnyNPCs(ModContent.NPCType<TbotMiniboss>()) && !NPC.AnyNPCs(ModContent.NPCType<ProtectorVoltNPC>()) && RedeWorld.downedJanitor && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface)
			{
				Main.tile[i, j];
				i -= 36;
				i *= 16;
				j *= 16;
				if (RedeWorld.downedVolt)
				{
					int k = NPC.NewNPC(i, j, ModContent.NPCType<ProtectorVoltNPC>(), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				else
				{
					int l = NPC.NewNPC(i, j, ModContent.NPCType<TbotMinibossStart>(), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, l, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			if (Main.netMode != 0 && !NPC.AnyNPCs(ModContent.NPCType<TbotMinibossStart>()) && !NPC.AnyNPCs(ModContent.NPCType<TbotMiniboss>()) && !NPC.AnyNPCs(ModContent.NPCType<ProtectorVoltNPC>()) && RedeWorld.downedJanitor && RedeWorld.downedStage3Scientist && RedeWorld.downedIBehemoth && RedeWorld.downedBlisterface)
			{
				ModPacket packet = base.mod.GetPacket(256);
				packet.Write(9);
				Utils.WriteVector2(packet, new Vector2((float)(i * 16), (float)(j * 16)));
				packet.Send(-1, -1);
			}
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			if (Main.netMode != 0)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.noThrow = 2;
				localPlayer.showItemIcon = true;
				localPlayer.showItemIcon2 = ModContent.ItemType<SignDeath>();
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
