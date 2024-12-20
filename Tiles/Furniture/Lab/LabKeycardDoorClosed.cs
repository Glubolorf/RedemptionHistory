using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Usable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class LabKeycardDoorClosed : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			TileID.Sets.NotReallySolid[(int)base.Type] = true;
			TileID.Sets.DrawsWalls[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorTop = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);
			TileObjectData.addAlternate(0);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 3);
			TileObjectData.addAlternate(0);
			TileObjectData.addTile((int)base.Type);
			base.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Keycard Door");
			base.AddMapEntry(new Color(189, 191, 200), name);
			this.minPick = 500;
			this.mineResist = 20f;
			this.dustType = 226;
			this.disableSmartCursor = true;
			this.adjTiles = new int[]
			{
				10
			};
			this.animationFrameHeight = 72;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if (Vector2.Distance(player.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f)) <= 1f)
			{
				player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " experienced DOOR STUCK."), 999999.0, 1, false);
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<Keycard>();
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 15)
			{
				frameCounter = 0;
				frame++;
				if (frame > 1)
				{
					frame = 0;
				}
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			if (Main.LocalPlayer.HasItem(ModContent.ItemType<Keycard>()))
			{
				Main.PlaySound(22, -1, -1, 1, 1f, 0f);
				int left = i - (int)(Main.tile[i, j].frameX / 18 % 1);
				int top = j - (int)(Main.tile[i, j].frameY / 18 % 4);
				WorldGen.KillTile(i, j, false, false, true);
				WorldGen.PlaceObject(i, j, ModContent.TileType<LabKeycardDoorOpen>(), false, 0, 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j, ModContent.TileType<LabKeycardDoorOpen>(), 0, 0, -1, -1);
				NetMessage.SendTileSquare(-1, left, top + 1, 2, 0);
			}
			return true;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
