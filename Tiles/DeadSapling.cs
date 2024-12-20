﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class DeadSapling : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(1, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				base.mod.TileType("DeadRockTile")
			};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.WaterPlacement = 1;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.addTile((int)base.Type);
			this.sapling = true;
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Petrified Sapling");
			base.AddMapEntry(new Color(200, 200, 200), modTranslation);
			this.dustType = 78;
			this.adjTiles = new int[]
			{
				20
			};
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void RandomUpdate(int i, int j)
		{
			if (WorldGen.genRand.Next(20) == 0)
			{
				bool flag = WorldGen.PlayerLOS(i, j);
				bool flag2 = WorldGen.GrowTree(i, j);
				if (flag2 && flag)
				{
					WorldGen.TreeGrowFXCheck(i, j);
				}
			}
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
		{
			if (i % 2 == 1)
			{
				effects = 1;
			}
		}
	}
}
