using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class RadioactiveSandTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			base.SetModCactus(new RadioactiveCactus());
			base.SetModPalmTree(new DeadPalmTree());
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("RadioactiveSandstoneTile")] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("HardenedRadioactiveSandTile")] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
			base.AddMapEntry(new Color(40, 60, 40), null);
			this.drop = base.mod.ItemType("RadioactiveSand");
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player localPlayer = Main.LocalPlayer;
			int num = (int)Vector2.Distance(localPlayer.Center / 16f, new Vector2((float)i, (float)j));
			if (num <= 15)
			{
				localPlayer.AddBuff(base.mod.BuffType("RadioactiveFalloutDebuff"), Main.rand.Next(10, 20), true);
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
