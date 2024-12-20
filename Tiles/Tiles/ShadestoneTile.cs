using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class ShadestoneTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneRubbleTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneHSlabTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneMossyTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<ShadestoneBrickMossyTile>()] = true;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.drop = ModContent.ItemType<Shadestone>();
			this.minPick = 350;
			this.mineResist = 11f;
			this.soundType = 21;
			base.CreateMapEntryName(null).SetDefault("Shadestone");
			base.AddMapEntry(new Color(30, 30, 30), null);
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			if (Utils.NextBool(Main.rand, 40000) && Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneSoulless)
			{
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 0, 0, ModContent.DustType<SoullessScreenDust>(), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
