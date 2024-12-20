using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Usable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class AncientGoldCoinPileTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[(int)base.Type] = false;
			Main.tileMergeDirt[(int)base.Type] = false;
			Main.tileLighted[(int)base.Type] = false;
			Main.tileBlockLight[(int)base.Type] = false;
			Main.tileShine[(int)base.Type] = 1100;
			TileID.Sets.Falling[(int)base.Type] = true;
			this.dustType = 246;
			this.soundType = 18;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(112, 88, 16), null);
			this.drop = ModContent.ItemType<AncientGoldCoin>();
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = (fail ? 1 : 3);
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
				int projectileType = ModContent.ProjectileType<AncientGoldCoinPileBall>();
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
