using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Redemption.Items.Placeable.Furniture.Misc;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Misc
{
	public class Mk2MicrobotFactoryTile : ModTile
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
			this.soundType = 21;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<Mk2MicrobotFactory>(), 1, false, 0, false, false);
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 8)
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
			Projectile.NewProjectile((float)i, (float)j, 0f, 0f, ModContent.ProjectileType<Mk2Microbot>(), 40, 1.5f, Main.LocalPlayer.whoAmI, 0f, 0f);
			Main.PlaySound(2, i, j, 53, 1f, 0f);
			Main.LocalPlayer.AddBuff(ModContent.BuffType<Mk2MicrobotBuff>(), 18000, true);
			return true;
		}
	}
}
