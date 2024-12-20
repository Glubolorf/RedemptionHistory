using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Usable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeStoneGateTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			TileID.Sets.NotReallySolid[(int)base.Type] = true;
			TileID.Sets.DrawsWalls[(int)base.Type] = true;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Height = 10;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.CoordinateWidth = 20;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Origin = new Point16(0, 9);
			TileObjectData.newTile.AnchorTop = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Shadestone Gate");
			base.AddMapEntry(new Color(50, 50, 50), name);
			this.minPick = 500;
			this.mineResist = 30f;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.animationFrameHeight = 180;
			this.disableSmartCursor = true;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (this._activated)
			{
				frame = 1;
				return;
			}
			frame = 0;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override bool NewRightClick(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			if (Main.LocalPlayer.HasItem(ModContent.ItemType<WardensKey>()) && !this._activated)
			{
				Main.PlaySound(22, -1, -1, 1, 1f, 0f);
				this._activated = true;
			}
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<WardensKey>();
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (this._activated)
			{
				Main.tileSolid[(int)base.Type] = false;
				Main.tileBlockLight[(int)base.Type] = false;
				return;
			}
			Main.tileSolid[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
		}

		private bool _activated;
	}
}
