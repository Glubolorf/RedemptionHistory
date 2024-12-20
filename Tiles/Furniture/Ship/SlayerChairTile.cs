using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Furniture.Ship;
using Redemption.NPCs.Bosses.KSIII;
using Redemption.NPCs.Friendly;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Ship
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
			Player p = Main.LocalPlayer;
			if (!NPC.AnyNPCs(ModContent.NPCType<KS3Sitting>()) && RedeWorld.downedSlayer && !RedeWorld.downedVlitch3 && !RedeWorld.downedNebuleus && !NPC.AnyNPCs(ModContent.NPCType<KS3_Body>()))
			{
				if (Main.netMode != 1)
				{
					int index = NPC.NewNPC((i + 2) * 16, (j + 5) * 16, ModContent.NPCType<KS3Sitting>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[index].netUpdate2 = true;
					return;
				}
				if (Main.netMode != 0)
				{
					Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 1, new object[]
					{
						(byte)p.whoAmI,
						ModContent.NPCType<KS3Sitting>(),
						(i + 2) * 16,
						(j + 5) * 16
					}).Send(-1, -1);
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
