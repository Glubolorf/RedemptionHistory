using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items.Placeable;
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
			Main.tileMerge[(int)base.Type][ModContent.TileType<LivingDeadLeavesTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<DeadGrassTile>()] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			base.AddMapEntry(new Color(90, 90, 90), null);
			this.mineResist = 2.5f;
			this.drop = ModContent.ItemType<DeadWood>();
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
