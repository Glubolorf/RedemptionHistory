using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Tiles.Tiles
{
	public class RadioactiveSandBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Sand Ball");
			ProjectileID.Sets.ForcePlateDetection[base.projectile.type] = new bool?(true);
		}

		public override void SetDefaults()
		{
			base.projectile.knockBack = 6f;
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.friendly = true;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			this.tileType = ModContent.TileType<RadioactiveSandTile>();
			this.dustType = ModContent.DustType<RadioactiveSandDust>();
		}

		public override void AI()
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, this.dustType, 0f, 0f, 0, default(Color), 1f);
				Dust dust2 = Main.dust[dust];
				dust2.velocity.X = dust2.velocity.X * 0.4f;
			}
			base.projectile.tileCollide = true;
			base.projectile.localAI[1] = 0f;
			if (base.projectile.ai[0] == 1f)
			{
				if (!this.falling)
				{
					base.projectile.ai[1] += 1f;
					if (base.projectile.ai[1] >= 60f)
					{
						base.projectile.ai[1] = 60f;
						Projectile projectile = base.projectile;
						projectile.velocity.Y = projectile.velocity.Y + 0.2f;
					}
				}
				else
				{
					Projectile projectile2 = base.projectile;
					projectile2.velocity.Y = projectile2.velocity.Y + 0.41f;
				}
			}
			else if (base.projectile.ai[0] == 2f)
			{
				Projectile projectile3 = base.projectile;
				projectile3.velocity.Y = projectile3.velocity.Y + 0.2f;
				if (base.projectile.velocity.X < -0.04f)
				{
					Projectile projectile4 = base.projectile;
					projectile4.velocity.X = projectile4.velocity.X + 0.04f;
				}
				else if (base.projectile.velocity.X > 0.04f)
				{
					Projectile projectile5 = base.projectile;
					projectile5.velocity.X = projectile5.velocity.X - 0.04f;
				}
				else
				{
					base.projectile.velocity.X = 0f;
				}
			}
			base.projectile.rotation += 0.1f;
			if (base.projectile.velocity.Y > 10f)
			{
				base.projectile.velocity.Y = 10f;
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			if (this.falling)
			{
				base.projectile.velocity = Collision.AnyCollision(base.projectile.position, base.projectile.velocity, base.projectile.width, base.projectile.height, true);
			}
			else
			{
				base.projectile.velocity = Collision.TileCollision(base.projectile.position, base.projectile.velocity, base.projectile.width, base.projectile.height, fallThrough, fallThrough, 1);
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			if (base.projectile.owner == Main.myPlayer && !base.projectile.noDropItem)
			{
				int tileX = (int)(base.projectile.position.X + (float)(base.projectile.width / 2)) / 16;
				int tileY = (int)(base.projectile.position.Y + (float)(base.projectile.width / 2)) / 16;
				Tile tile = Main.tile[tileX, tileY];
				Tile tileBelow = Main.tile[tileX, tileY + 1];
				if (tile.halfBrick() && base.projectile.velocity.Y > 0f && Math.Abs(base.projectile.velocity.Y) > Math.Abs(base.projectile.velocity.X))
				{
					tileY--;
				}
				if (!tile.active())
				{
					bool flag = tileY < Main.maxTilesY - 2 && tileBelow != null && tileBelow.active() && tileBelow.type == 314;
					if (!flag)
					{
						WorldGen.PlaceTile(tileX, tileY, this.tileType, false, true, -1, 0);
					}
					if (!flag && tile.active() && (int)tile.type == this.tileType)
					{
						if (tileBelow.halfBrick() || tileBelow.slope() != 0)
						{
							WorldGen.SlopeTile(tileX, tileY + 1, 0);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(17, -1, -1, null, 14, (float)tileX, (float)(tileY + 1), 0f, 0, 0, 0);
							}
						}
						if (Main.netMode != 0)
						{
							NetMessage.SendData(17, -1, -1, null, 1, (float)tileX, (float)tileY, (float)this.tileType, 0, 0, 0);
						}
					}
				}
			}
		}

		public override bool CanDamage()
		{
			return base.projectile.localAI[1] != -1f;
		}

		protected bool falling = true;

		protected int tileType;

		protected int dustType;
	}
}
