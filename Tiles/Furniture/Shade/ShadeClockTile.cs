using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Placeable.Furniture.Shade;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeClockTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileID.Sets.HasOutlines[(int)base.Type] = true;
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
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Shade Clock");
			base.AddMapEntry(new Color(80, 80, 80), name);
			this.dustType = ModContent.DustType<VoidFlame>();
			this.adjTiles = new int[]
			{
				104
			};
		}

		public override bool NewRightClick(int i, int j)
		{
			string text = "AM";
			double time = Main.time;
			if (!Main.dayTime)
			{
				time += 54000.0;
			}
			time = time / 86400.0 * 24.0;
			time = time - 7.5 - 12.0;
			if (time < 0.0)
			{
				time += 24.0;
			}
			if (time >= 12.0)
			{
				text = "PM";
			}
			int intTime = (int)time;
			double num = (double)((int)((time - (double)intTime) * 60.0));
			string text2 = string.Concat(num);
			if (num < 10.0)
			{
				text2 = "0" + text2;
			}
			if (intTime > 12)
			{
				intTime -= 12;
			}
			if (intTime == 0)
			{
				intTime = 12;
			}
			string newText = string.Concat(new object[]
			{
				Language.GetTextValue("CLI.Time_Command") + ": ",
				intTime,
				":",
				text2,
				" ",
				text
			});
			if (Main.netMode != 1)
			{
				BaseUtility.Chat(newText, byte.MaxValue, 240, 20, true);
			}
			return true;
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
			Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<ShadeClock>(), 1, false, 0, false, false);
		}
	}
}
