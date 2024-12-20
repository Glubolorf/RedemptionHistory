﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class TurretTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.AnchorTop = new AnchorData(9, 1, 1);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 7;
			this.minPick = 500;
			this.mineResist = 8f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(130, 120, 130), null);
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
					if (!RedeWorld.labSafe && !NPC.AnyNPCs(base.mod.NPCType("LabSentryTurretLeg")))
					{
						Main.tile[i, j];
						i *= 16;
						j++;
						j *= 16;
						int k = NPC.NewNPC(i + 8, j + 8, base.mod.NPCType("LabSentryTurretLeg"), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
					}
				}
				else if (!RedeWorld.labSafe && !NPC.AnyNPCs(base.mod.NPCType("LabSentryTurretLeg")))
				{
					ModPacket packet = base.mod.GetPacket(256);
					packet.Write(20);
					Utils.WriteVector2(packet, new Vector2((float)(i * 16) + 0.5f, (float)(j * 16) + 1.5f));
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