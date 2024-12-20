using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items.Materials.PostML;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Ores
{
	public class MasksTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			this.dustType = ModContent.DustType<VoidFlame>();
			this.minPick = 300;
			this.mineResist = 6f;
			this.soundType = 50;
			this.soundStyle = base.mod.GetSoundSlot(50, "Sounds/Custom/MaskBreak");
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(130, 140, 150), null);
		}

		public override bool Drop(int i, int j)
		{
			if (Main.rand.Next(8) == 0)
			{
				Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<VesselFrag>(), 1, false, 0, false, false);
			}
			return true;
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			if (Utils.NextBool(Main.rand, 4000) && Main.LocalPlayer.GetModPlayer<RedePlayer>().ZoneSoulless)
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
