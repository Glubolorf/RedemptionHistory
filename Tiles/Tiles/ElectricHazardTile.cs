using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class ElectricHazardTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileLighted[(int)base.Type] = true;
			this.dustType = 226;
			this.minPick = 310;
			this.mineResist = 7f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(200, 255, 255), null);
			this.animationFrameHeight = 90;
			this.drop = ModContent.ItemType<ElectricHazard>();
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 4)
			{
				frameCounter = 0;
				frame++;
				if (frame > 2)
				{
					frame = 0;
				}
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if (Vector2.Distance(player.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f)) <= 1f)
			{
				player.AddBuff(144, 120, true);
			}
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.3f;
			g = 0.3f;
			b = 0.5f;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
