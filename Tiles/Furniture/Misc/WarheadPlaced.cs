using System;
using Microsoft.Xna.Framework;
using Redemption.Items;
using Redemption.Items.Placeable.Furniture.Misc;
using Redemption.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Misc
{
	public class WarheadPlaced : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = 1;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = 2;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile((int)base.Type);
			this.dustType = 226;
			this.minPick = 10;
			this.mineResist = 7f;
			this.disableSmartCursor = true;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(62, 88, 90), null);
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<LabHelpMessage>();
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			if (!RedeWorld.nukeCountdownActive)
			{
				Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<WarheadItem>(), 1, false, 0, false, false);
				return;
			}
			RedeWorld.nukeTimerInternal = 2;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override bool NewRightClick(int i, int j)
		{
			if (!RedeWorld.nukeCountdownActive)
			{
				if (!Main.dedServ)
				{
					NukeDetonationUI.Visible = true;
				}
				RedeWorld.nukeGroundZero = new Vector2((float)(i * 16), (float)(j * 16));
				if (Main.netMode != 0)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			return true;
		}
	}
}
