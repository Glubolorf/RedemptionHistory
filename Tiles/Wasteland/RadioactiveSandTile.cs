using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items.Placeable.Wasteland;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class RadioactiveSandTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileBrick[(int)base.Type] = true;
			base.SetModCactus(new RadioactiveCactus());
			base.SetModPalmTree(new DeadPalmTree());
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<RadioactiveSandstoneTile>()] = true;
			Main.tileMerge[(int)base.Type][ModContent.TileType<HardenedRadioactiveSandTile>()] = true;
			Main.tileBlendAll[(int)base.Type] = true;
			this.soundStyle = 18;
			Main.tileSand[(int)base.Type] = true;
			TileID.Sets.TouchDamageSands[(int)base.Type] = 15;
			TileID.Sets.Conversion.Sand[(int)base.Type] = true;
			TileID.Sets.ForAdvancedCollision.ForSandshark[(int)base.Type] = true;
			TileID.Sets.Falling[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			base.AddMapEntry(new Color(40, 60, 40), null);
			this.drop = ModContent.ItemType<RadioactiveSand>();
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if ((int)Vector2.Distance(player.Center / 16f, new Vector2((float)i, (float)j)) <= 15)
			{
				player.AddBuff(ModContent.BuffType<RadioactiveFalloutDebuff>(), Main.rand.Next(10, 20), true);
			}
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = base.mod.GetWaterfallStyleSlot("XenoWaterfallStyle");
		}

		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			if (WorldGen.noTileActions)
			{
				return true;
			}
			Tile above = Main.tile[i, j - 1];
			Tile below = Main.tile[i, j + 1];
			bool canFall = true;
			if (below == null || below.active())
			{
				canFall = false;
			}
			if (above.active() && (TileID.Sets.BasicChest[(int)above.type] || TileID.Sets.BasicChestFake[(int)above.type] || above.type == 323 || TileLoader.IsDresser((int)above.type)))
			{
				canFall = false;
			}
			if (canFall)
			{
				int projectileType = ModContent.ProjectileType<RadioactiveSandBall>();
				float positionX = (float)(i * 16 + 8);
				float positionY = (float)(j * 16 + 8);
				if (Main.netMode == 0)
				{
					Main.tile[i, j].ClearTile();
					int proj = Projectile.NewProjectile(positionX, positionY, 0f, 0.41f, projectileType, 10, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[proj].ai[0] = 1f;
					WorldGen.SquareTileFrame(i, j, true);
				}
				else if (Main.netMode == 2)
				{
					Main.tile[i, j].active(false);
					bool spawnProj = true;
					for (int k = 0; k < 1000; k++)
					{
						Projectile otherProj = Main.projectile[k];
						if (otherProj.active && otherProj.owner == Main.myPlayer && otherProj.type == projectileType && Math.Abs(otherProj.timeLeft - 3600) < 60 && otherProj.Distance(new Vector2(positionX, positionY)) < 4f)
						{
							spawnProj = false;
							break;
						}
					}
					if (spawnProj)
					{
						int proj2 = Projectile.NewProjectile(positionX, positionY, 0f, 2.5f, projectileType, 10, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[proj2].velocity.Y = 0.5f;
						Projectile projectile = Main.projectile[proj2];
						projectile.position.Y = projectile.position.Y + 2f;
						Main.projectile[proj2].netUpdate = true;
					}
					NetMessage.SendTileSquare(-1, i, j, 1, 0);
					WorldGen.SquareTileFrame(i, j, true);
				}
				return false;
			}
			return true;
		}
	}
}
