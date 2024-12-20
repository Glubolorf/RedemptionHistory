using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
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
			if (Main.netMode != 1)
			{
				Player localPlayer = Main.LocalPlayer;
				float num = Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
				if (num <= 12f && num > 5f && Main.rand.Next(300) == 0 && NPC.CountNPCS(base.mod.NPCType("SludgyBoi2")) <= 4)
				{
					i *= 16;
					j *= 16;
					int num2 = NPC.NewNPC(i + 1, j + 2, base.mod.NPCType("SludgyBoi2"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		private int sludgeCount;

		private int sludgeCooldown;
	}
}
