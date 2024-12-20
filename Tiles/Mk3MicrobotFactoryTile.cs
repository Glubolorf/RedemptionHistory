using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class Mk3MicrobotFactoryTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Width = 5;
			TileObjectData.newTile.Origin = new Point16(2, 2);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			base.AddMapEntry(new Color(175, 13, 166), null);
			this.animationFrameHeight = 72;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, base.mod.ItemType("Mk3MicrobotFactory"), 1, false, 0, false, false);
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 8)
			{
				frameCounter = 0;
				frame++;
				if (frame > 3)
				{
					frame = 0;
				}
			}
		}

		public override void RightClick(int x, int y)
		{
			x *= 16;
			y *= 16;
			Projectile.NewProjectile((float)x, (float)y, 0f, 0f, base.mod.ProjectileType("Mk3Microbot"), 100, 1.5f, Main.LocalPlayer.whoAmI, 0f, 0f);
			Main.PlaySound(2, x, y, 53, 1f, 0f);
			Main.LocalPlayer.AddBuff(base.mod.BuffType("Mk3MicrobotBuff"), 18000, true);
		}
	}
}
