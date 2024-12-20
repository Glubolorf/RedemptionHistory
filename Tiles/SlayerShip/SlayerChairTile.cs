using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable;
using Redemption.NPCs.Bosses.KingSlayerIII;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.SlayerShip
{
	public class SlayerChairTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.dustType = 7;
			this.minPick = 500;
			this.mineResist = 10f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(110, 110, 130), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player localPlayer = Main.LocalPlayer;
			RedePlayer redePlayer = (RedePlayer)localPlayer.GetModPlayer(base.mod, "RedePlayer");
			if ((int)Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i, (float)j)) <= 100)
			{
				if (Main.netMode == 0)
				{
					if (!NPC.AnyNPCs(ModContent.NPCType<KS3Sitting>()) && RedeWorld.downedSlayer && !RedeWorld.downedVlitch3 && !RedeWorld.downedNebuleus && !NPC.AnyNPCs(ModContent.NPCType<KSEntrance>()))
					{
						Main.tile[i, j];
						i += 2;
						i *= 16;
						j += 5;
						j *= 16;
						int k = NPC.NewNPC(i + 1, j + 1, ModContent.NPCType<KS3Sitting>(), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
					}
				}
				else if (!NPC.AnyNPCs(ModContent.NPCType<KS3Sitting>()) && RedeWorld.downedSlayer && !RedeWorld.downedVlitch3 && !RedeWorld.downedNebuleus && !NPC.AnyNPCs(ModContent.NPCType<KSEntrance>()))
				{
					ModPacket packet = base.mod.GetPacket(256);
					packet.Write(16);
					Utils.WriteVector2(packet, new Vector2((float)(i * 16 + 2), (float)(j * 16 + 5)));
					packet.Send(-1, -1);
				}
			}
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return RedeWorld.downedVlitch3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<SlayerChair>(), 1, false, 0, false, false);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
