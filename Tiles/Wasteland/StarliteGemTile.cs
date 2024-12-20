using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Wasteland
{
	public class StarliteGemTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = false;
			Main.tileLighted[(int)base.Type] = true;
			this.dustType = 61;
			this.drop = ModContent.ItemType<Starlite>();
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Starlite");
			base.AddMapEntry(new Color(30, 180, 90), name);
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				ModContent.TileType<RadioactiveSandstoneTile>(),
				ModContent.TileType<DeadRockTile>()
			};
			TileObjectData.addTile((int)base.Type);
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
			if (i % 10 < 4)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			frameXOffset = i % 4 * 18;
		}

		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 4;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 0.3f;
			b = 0.1f;
		}
	}
}
