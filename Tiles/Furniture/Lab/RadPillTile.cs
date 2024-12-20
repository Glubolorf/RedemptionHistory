using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Usable.Potions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class RadPillTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = false;
			Main.tileTable[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(15, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.drop = ModContent.ItemType<RadiationPill>();
			this.dustType = 226;
			this.minPick = 0;
			this.mineResist = 1f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(200, 50, 50), null);
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<RadiationPill>();
		}

		public override bool NewRightClick(int i, int j)
		{
			WorldGen.KillTile(i, j, false, false, false);
			if (Main.netMode == 1)
			{
				NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 1f, 0, 0, 0);
			}
			return true;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
