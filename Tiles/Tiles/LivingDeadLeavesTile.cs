﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class LivingDeadLeavesTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileMerge[(int)base.Type][ModContent.TileType<LivingDeadWoodTile>()] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			TileID.Sets.Leaves[(int)base.Type] = true;
			base.AddMapEntry(new Color(100, 100, 100), null);
			this.mineResist = 1f;
			this.dustType = 206;
			this.soundType = 6;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 15)
			{
				player.AddBuff(ModContent.BuffType<RadioactiveFalloutDebuff>(), Main.rand.Next(10, 20), true);
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return true;
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}
	}
}
