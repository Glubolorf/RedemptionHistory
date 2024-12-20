using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class OperatorSpawner : ModTile
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
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			this.dustType = 7;
			this.minPick = 300;
			this.mineResist = 7f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(180, 150, 185), null);
			this.adjTiles = new int[]
			{
				14
			};
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player localPlayer = Main.LocalPlayer;
			RedePlayer redePlayer = (RedePlayer)localPlayer.GetModPlayer(base.mod, "RedePlayer");
			if ((int)Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i, (float)j)) <= 100)
			{
				if (Main.netMode == 0)
				{
					if (!NPC.AnyNPCs(base.mod.NPCType("MACEControllerIdle")) && !RedeWorld.downedMACE && RedeWorld.labSafe)
					{
						Main.tile[i, j];
						i += 3;
						i *= 16;
						j += 2;
						j *= 16;
						int k = NPC.NewNPC(i, j, base.mod.NPCType("MACEControllerIdle"), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
					}
				}
				else if (!NPC.AnyNPCs(base.mod.NPCType("MACEControllerIdle")) && !RedeWorld.downedMACE && RedeWorld.labSafe)
				{
					ModPacket packet = base.mod.GetPacket(256);
					packet.Write(21);
					Utils.WriteVector2(packet, new Vector2((float)(i * 16 + 3), (float)(j * 16 + 2)));
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
