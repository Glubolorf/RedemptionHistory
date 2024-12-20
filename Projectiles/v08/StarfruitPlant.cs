using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class StarfruitPlant : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starfruit Plant");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 54;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 7)
				{
					base.projectile.frame = 4;
				}
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			if (base.projectile.ai[0] == 1f)
			{
				this.cloudCounter++;
				if (this.cloudCounter > 15)
				{
					this.cloudFrame++;
					this.cloudCounter = 0;
				}
				if (this.cloudFrame >= 2)
				{
					this.cloudFrame = 0;
				}
				Projectile projectile2 = base.projectile;
				projectile2.velocity.Y = projectile2.velocity.Y * 0f;
			}
			else
			{
				Projectile projectile3 = base.projectile;
				projectile3.velocity.Y = projectile3.velocity.Y + 1f;
			}
			if (base.projectile.frame >= 5)
			{
				base.projectile.localAI[1] += 1f;
				if (base.projectile.localAI[1] == 30f)
				{
					int num = 8;
					for (int i = 0; i < num; i++)
					{
						int num2 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("StarfruitPro"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
						Main.projectile[num2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)i / (float)num * 6.28f);
						Main.npc[num2].netUpdate = true;
					}
				}
				if (base.projectile.localAI[1] == 60f)
				{
					int num3 = 8;
					for (int j = 0; j < num3; j++)
					{
						int num4 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("StarfruitPro2"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
						Main.projectile[num4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)j / (float)num3 * 6.28f);
						Main.npc[num4].netUpdate = true;
					}
				}
				if (base.projectile.localAI[1] >= 60f)
				{
					base.projectile.localAI[1] = 0f;
				}
			}
			if (base.projectile.localAI[0] > 180f)
			{
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0f)
			{
				base.projectile.velocity.X = oldVelocity.X * --0f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * --0f;
			}
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.projectileTexture[base.projectile.type];
			Texture2D texture = base.mod.GetTexture("Projectiles/v08/PlantCloud");
			int spriteDirection = base.projectile.spriteDirection;
			Vector2 vector;
			vector..ctor(base.projectile.Center.X, base.projectile.Center.Y);
			int num = texture2D.Height / 7;
			int num2 = num * base.projectile.frame;
			Main.spriteBatch.Draw(texture2D, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2D.Width, num)), drawColor, base.projectile.rotation, new Vector2((float)texture2D.Width / 2f, (float)num / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? 0 : 1, 0f);
			if (base.projectile.ai[0] == 1f)
			{
				Vector2 vector2;
				vector2..ctor(base.projectile.Center.X, base.projectile.Center.Y + 32f);
				int num3 = texture.Height / 2;
				int num4 = num3 * this.cloudFrame;
				Main.spriteBatch.Draw(texture, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture.Width, num3)), drawColor, base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num3 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public int cloudFrame;

		public int cloudCounter;
	}
}
