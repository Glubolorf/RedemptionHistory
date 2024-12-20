using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
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
			Player p = Main.LocalPlayer;
			if (!RedeWorld.labSafe && !NPC.AnyNPCs(ModContent.NPCType<LabSentryTurretLeg>()))
			{
				if (Main.netMode != 1)
				{
					int index = NPC.NewNPC((int)((double)i + 0.5) * 16, (int)((double)j + 1.5) * 16, ModContent.NPCType<LabSentryTurretLeg>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[index].netUpdate2 = true;
					return;
				}
				if (Main.netMode != 0)
				{
					Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 1, new object[]
					{
						(byte)p.whoAmI,
						ModContent.NPCType<LabSentryTurretLeg>(),
						(int)((double)i + 0.5) * 16,
						(int)((double)j + 1.5) * 16
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
