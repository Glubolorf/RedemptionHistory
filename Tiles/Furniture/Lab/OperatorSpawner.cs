using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
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
			this.minPick = 500;
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
			Player p = Main.LocalPlayer;
			if (!NPC.AnyNPCs(ModContent.NPCType<MACEControllerIdle>()) && !RedeWorld.downedMACE && RedeWorld.labSafe)
			{
				if (Main.netMode != 1)
				{
					int index = NPC.NewNPC((i + 3) * 16, (j + 2) * 16, ModContent.NPCType<MACEControllerIdle>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[index].netUpdate2 = true;
					return;
				}
				if (Main.netMode != 0)
				{
					Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 1, new object[]
					{
						(byte)p.whoAmI,
						ModContent.NPCType<MACEControllerIdle>(),
						(i + 3) * 16,
						(j + 2) * 16
					}).Send(-1, -1);
				}
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
