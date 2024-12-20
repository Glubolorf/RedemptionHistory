using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant2 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skyflower");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 36;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 170;
		}

		protected override void PlantAI()
		{
			Player player = Main.player[base.projectile.owner];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				if (this.IsOnNativeTerrain)
				{
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 8)
					{
						base.projectile.frame = 7;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 7)
					{
						base.projectile.frame = 6;
					}
				}
			}
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
				this.IsOnNativeTerrain = true;
				Projectile projectile4 = base.projectile;
				projectile4.velocity.Y = projectile4.velocity.Y * 0f;
			}
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 800f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 15f == 0f && base.projectile.frame >= 5)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 2f, base.projectile.position.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), base.mod.ProjectileType("SkyflowerPetal"), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
			}
			if (this.IsOnNativeTerrain && Main.rand.Next(5) == 0)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-32, 32), base.projectile.Bottom.Y + 6f), new Vector2(0f, 0f), base.mod.ProjectileType("SkyflowerRainPro"), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D cloudAni = base.mod.GetTexture("Projectiles/DruidProjectiles/Plants/PlantCloud");
			int spriteDirection = base.projectile.spriteDirection;
			Vector2 drawCenter2 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y);
			int num215 = texture.Height / 8;
			int y7 = num215 * base.projectile.frame;
			Main.spriteBatch.Draw(texture, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), drawColor, base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			if (base.projectile.ai[0] == 1f)
			{
				Vector2 drawCenter3 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y + 32f);
				int num216 = cloudAni.Height / 2;
				int y8 = num216 * this.cloudFrame;
				Main.spriteBatch.Draw(cloudAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, cloudAni.Width, num216)), drawColor, base.projectile.rotation, new Vector2((float)cloudAni.Width / 2f, (float)num216 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public int cloudFrame;

		public int cloudCounter;

		private NPC target;
	}
}
