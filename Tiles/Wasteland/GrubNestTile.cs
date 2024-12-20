using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Wasteland
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
			TileObjectData.addTile((int)base.Type);
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Grub Nest");
			base.AddMapEntry(new Color(40, 60, 40), modTranslation);
			this.disableSmartCursor = true;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (Main.netMode != 1)
			{
				Player localPlayer = Main.LocalPlayer;
				float num = Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
				if (num <= 12f && num > 5f && Main.rand.Next(100) == 0)
				{
					i++;
					i *= 16;
					j++;
					j *= 16;
					int num2 = NPC.NewNPC(i, j, base.mod.NPCType("InfectedGrub"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
					}
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
