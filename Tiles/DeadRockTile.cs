using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles
{
	public class DeadRockTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileSpelunker[(int)base.Type] = false;
			Main.tileNoSunLight[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			this.drop = base.mod.ItemType("DeadRock");
			this.minPick = 180;
			this.mineResist = 2.5f;
			this.soundType = 21;
			base.AddMapEntry(new Color(30, 50, 25), null);
			base.SetModTree(new DeadTree());
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.AddBuff(base.mod.BuffType("RadioactiveFalloutDebuff"), Main.rand.Next(10, 20), true);
			}
		}

		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return base.mod.TileType("DeadSapling");
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}
	}
}
