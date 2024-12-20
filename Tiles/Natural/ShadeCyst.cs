using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Weapons.PostML.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Natural
{
	public class ShadeCyst : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolidTop[(int)base.Type] = false;
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.Width = 5;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 0;
			TileObjectData.newTile.Origin = new Point16(2, 2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.dustType = 261;
			this.soundStyle = 13;
			this.soundType = 3;
			this.mineResist = 10f;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(210, 200, 191), null);
			this.disableSmartCursor = true;
			this.animationFrameHeight = 48;
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

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			for (int k = 0; k < Main.rand.Next(6, 12); k++)
			{
				if (Main.netMode != 1)
				{
					int p = Projectile.NewProjectile(new Vector2(((float)i + Utils.NextFloat(Main.rand, 1f, 4f)) * 16f, ((float)j + Utils.NextFloat(Main.rand, 1f, 2.5f)) * 16f), RedeHelper.PolarVector((float)Main.rand.Next(3, 14), Utils.NextFloat(Main.rand, 0f, 6.2831855f)), ModContent.ProjectileType<EchoF>(), 300, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].melee = false;
				}
			}
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
	}
}
