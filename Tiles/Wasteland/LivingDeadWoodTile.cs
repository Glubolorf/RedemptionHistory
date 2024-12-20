using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class LivingDeadWoodTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("LivingDeadLeavesTile")] = true;
			Main.tileMerge[(int)base.Type][base.mod.TileType("DeadGrassTile")] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			base.AddMapEntry(new Color(90, 90, 90), null);
			this.mineResist = 2.5f;
			this.drop = base.mod.ItemType("DeadWood");
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
