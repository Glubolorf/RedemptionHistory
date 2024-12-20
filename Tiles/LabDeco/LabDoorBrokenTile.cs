using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.LabDeco;
using Redemption.NPCs.LabNPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class LabDoorBrokenTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
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
			base.AddMapEntry(new Color(180, 180, 185), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (Main.netMode != 1)
			{
				float dist = Vector2.Distance(Main.LocalPlayer.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
				if (dist <= 12f && dist > 6f)
				{
					if (Main.rand.Next(350) == 0 && NPC.CountNPCS(ModContent.NPCType<WalterInfected>()) <= 3)
					{
						i *= 16;
						j *= 16;
						int k = NPC.NewNPC(i + 2, j + 2, ModContent.NPCType<WalterInfected>(), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
						}
					}
					if (NPC.downedMoonlord && Main.rand.Next(600) == 0 && NPC.CountNPCS(ModContent.NPCType<Stage2Scientist>()) == 0)
					{
						i *= 16;
						j *= 16;
						int l = NPC.NewNPC(i + 2, j + 2, ModContent.NPCType<Stage2Scientist>(), 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, l, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<LabDoor2>(), 1, false, 0, false, false);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
