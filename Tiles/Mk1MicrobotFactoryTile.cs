using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items.Placeable;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class Mk1MicrobotFactoryTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			base.AddMapEntry(new Color(175, 13, 166), null);
			this.animationFrameHeight = 54;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<Mk1MicrobotFactory>(), 1, false, 0, false, false);
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 10)
			{
				frameCounter = 0;
				frame++;
				if (frame > 5)
				{
					frame = 0;
				}
			}
		}

		public override bool NewRightClick(int i, int j)
		{
			i *= 16;
			j *= 16;
			Projectile.NewProjectile((float)i, (float)j, 0f, 0f, ModContent.ProjectileType<Mk1Microbot>(), 15, 1.5f, Main.LocalPlayer.whoAmI, 0f, 0f);
			Main.PlaySound(2, i, j, 53, 1f, 0f);
			Main.LocalPlayer.AddBuff(ModContent.BuffType<Mk1MicrobotBuff>(), 18000, true);
			return true;
		}
	}
}
