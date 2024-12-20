using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class JanitorSpawner : ModTile
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
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.dustType = 7;
			this.minPick = 500;
			this.mineResist = 7f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(130, 20, 30), null);
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
					if (!NPC.AnyNPCs(base.mod.NPCType("JanitorBot")) && !NPC.AnyNPCs(base.mod.NPCType("JanitorBotCleaning")) && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !RedeWorld.downedJanitor && !RedeWorld.labAccess1)
					{
						Main.tile[i, j];
						i *= 16;
						j *= 16;
						int k = NPC.NewNPC(i + 1, j + 1, base.mod.NPCType("JanitorBotCleaning"), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
					}
				}
				else if (!NPC.AnyNPCs(base.mod.NPCType("JanitorBot")) && !NPC.AnyNPCs(base.mod.NPCType("JanitorBotCleaning")) && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !RedeWorld.downedJanitor && !RedeWorld.labAccess1)
				{
					ModPacket packet = base.mod.GetPacket(256);
					packet.Write(17);
					Utils.WriteVector2(packet, new Vector2((float)(i * 16), (float)(j * 16)));
					packet.Send(-1, -1);
				}
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
