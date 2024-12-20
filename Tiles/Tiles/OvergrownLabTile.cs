using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Placeable.Tiles;
using Redemption.Tiles.Plants;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class OvergrownLabTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<LabTileUnsafe>()] = true;
			this.dustType = 226;
			this.drop = ModContent.ItemType<LabPlating>();
			this.minPick = 500;
			this.mineResist = 3f;
			this.soundType = 21;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(200, 200, 200), null);
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 15)
			{
				player.AddBuff(ModContent.BuffType<RadioactiveFalloutDebuff>(), Main.rand.Next(10, 20), true);
			}
		}

		public override void RandomUpdate(int i, int j)
		{
			if (!Framing.GetTileSafely(i, j - 1).active() && Main.rand.Next(10) == 0 && Main.tile[i, j - 1].liquid == 0)
			{
				WorldGen.PlaceObject(i, j - 1, ModContent.TileType<LabShrub>(), true, Main.rand.Next(7), 0, -1, -1);
				NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<LabShrub>(), Main.rand.Next(7), 0, -1, -1);
			}
			if (Utils.NextBool(Main.rand, 45))
			{
				WorldGen.SpreadGrass(i + Main.rand.Next(-1, 1), j + Main.rand.Next(-1, 1), ModContent.TileType<LabTileUnsafe>(), ModContent.TileType<OvergrownLabTile>(), false, 0);
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

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return RedeWorld.downedPatientZero;
		}
	}
}
