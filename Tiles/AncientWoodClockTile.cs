using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles
{
	public class AncientWoodClockTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16,
				16
			};
			TileObjectData.addTile((int)base.Type);
			ModTranslation modTranslation = base.CreateMapEntryName(null);
			modTranslation.SetDefault("Ancient Wood Clock");
			base.AddMapEntry(new Color(200, 200, 200), modTranslation);
			this.dustType = 78;
			this.adjTiles = new int[]
			{
				104
			};
		}

		public override void RightClick(int x, int y)
		{
			string text = "AM";
			double num = Main.time;
			if (!Main.dayTime)
			{
				num += 54000.0;
			}
			num = num / 86400.0 * 24.0;
			num = num - 7.5 - 12.0;
			if (num < 0.0)
			{
				num += 24.0;
			}
			if (num >= 12.0)
			{
				text = "PM";
			}
			int num2 = (int)num;
			double num3 = num - (double)num2;
			num3 = (double)((int)(num3 * 60.0));
			string text2 = string.Concat(num3);
			if (num3 < 10.0)
			{
				text2 = "0" + text2;
			}
			if (num2 > 12)
			{
				num2 -= 12;
			}
			if (num2 == 0)
			{
				num2 = 12;
			}
			string text3 = string.Concat(new object[]
			{
				"Time: ",
				num2,
				":",
				text2,
				" ",
				text
			});
			Main.NewText(text3, byte.MaxValue, 240, 20, false);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				Main.clock = true;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, base.mod.ItemType("AncientWoodClock"), 1, false, 0, false, false);
		}
	}
}
