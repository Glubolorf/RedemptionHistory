using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class VentTile4 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 3;
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
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 7;
			this.minPick = 500;
			this.mineResist = 3f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(40, 40, 40), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			float dist = Vector2.Distance(player.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
			if (dist <= 12f && dist > 5f && Main.rand.Next(300) == 0 && NPC.CountNPCS(ModContent.NPCType<SludgyBoi2>()) <= 4)
			{
				if (Main.netMode != 1)
				{
					int index = NPC.NewNPC((i + 1) * 16, (j + 2) * 16, ModContent.NPCType<SludgyBoi2>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[index].netUpdate2 = true;
					return;
				}
				if (Main.netMode != 0)
				{
					Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 2, new object[]
					{
						(byte)player.whoAmI,
						ModContent.NPCType<SludgyBoi2>(),
						(i + 1) * 16,
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
