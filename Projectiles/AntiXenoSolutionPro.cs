using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class AntiXenoSolutionPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Anti-Xeno Spray");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 6;
			base.projectile.height = 6;
			base.projectile.friendly = true;
			base.projectile.alpha = 255;
			base.projectile.penetrate = -1;
			base.projectile.extraUpdates = 2;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			int num = base.mod.DustType("AntiXenoSolutionDust");
			if (base.projectile.owner == Main.myPlayer)
			{
				this.Convert((int)(base.projectile.position.X + (float)(base.projectile.width / 2)) / 16, (int)(base.projectile.position.Y + (float)(base.projectile.height / 2)) / 16, 2);
			}
			if (base.projectile.timeLeft > 133)
			{
				base.projectile.timeLeft = 133;
			}
			if (base.projectile.ai[0] > 7f)
			{
				float num2 = 1f;
				if (base.projectile.ai[0] == 8f)
				{
					num2 = 0.2f;
				}
				else if (base.projectile.ai[0] == 9f)
				{
					num2 = 0.4f;
				}
				else if (base.projectile.ai[0] == 10f)
				{
					num2 = 0.6f;
				}
				else if (base.projectile.ai[0] == 11f)
				{
					num2 = 0.8f;
				}
				base.projectile.ai[0] += 1f;
				for (int i = 0; i < 1; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, num, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
					Dust dust = Main.dust[num3];
					dust.noGravity = true;
					dust.scale *= 1.75f;
					dust.velocity.X = dust.velocity.X * 2f;
					dust.velocity.Y = dust.velocity.Y * 2f;
					dust.scale *= num2;
				}
			}
			else
			{
				base.projectile.ai[0] += 1f;
			}
			base.projectile.rotation += 0.3f * (float)base.projectile.direction;
		}

		public void Convert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && (double)(Math.Abs(k - i) + Math.Abs(l - j)) < Math.Sqrt((double)(size * size + size * size)))
					{
						int type = (int)Main.tile[k, l].type;
						ushort wall = Main.tile[k, l].wall;
						if (type == base.mod.WallType("DeadRockWallTile"))
						{
							Main.tile[k, l].wall = 2;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
						else if (type == base.mod.TileType("DeadRockTile"))
						{
							Main.tile[k, l].type = 2;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, 0);
						}
					}
				}
			}
		}
	}
}
