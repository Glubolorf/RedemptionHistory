using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Wasteland;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Natural
{
	public class GrubNestTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				ModContent.TileType<RadioactiveSandstoneTile>()
			};
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Grub Nest");
			base.AddMapEntry(new Color(40, 60, 40), name);
			this.disableSmartCursor = true;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			float dist = Vector2.Distance(Main.LocalPlayer.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
			if (dist <= 12f && dist > 5f && Main.rand.Next(100) == 0)
			{
				int index = NPC.NewNPC((int)(((float)i + 1f) * 16f), (int)(((float)j + 1f) * 16f), ModContent.NPCType<InfectedGrub>(), 0, 0f, 0f, 0f, 0f, 255);
				if (index < 200 && Main.netMode == 1)
				{
					NetMessage.SendData(23, -1, -1, null, index, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 4;
		}
	}
}
