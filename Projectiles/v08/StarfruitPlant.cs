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
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 7)
				{
					base.projectile.frame = 4;
				}
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile3 = base.projectile;
			projectile3.velocity.X = projectile3.velocity.X * 0f;
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
				Projectile projectile4 = base.projectile;
				projectile4.velocity.Y = projectile4.velocity.Y * 0f;
			}
			else
			{
				Projectile projectile5 = base.projectile;
				projectile5.velocity.Y = projectile5.velocity.Y + 1f;
			}
			if (base.projectile.frame >= 5)
			{
				base.projectile.localAI[1] += 1f;
				if (base.projectile.localAI[1] == 30f)
				{
					int pieCut = 8;
					for (int i = 0; i < pieCut; i++)
					{
						int projID = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("StarfruitPro"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
						Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)i / (float)pieCut * 6.28f);
						Main.npc[projID].netUpdate = true;
					}
				}
				if (base.projectile.localAI[1] == 60f)
				{
					int pieCut2 = 8;
					for (int j = 0; j < pieCut2; j++)
					{
						int projID2 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("StarfruitPro2"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
						Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)j / (float)pieCut2 * 6.28f);
						Main.npc[projID2].netUpdate = true;
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
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D cloudAni = base.mod.GetTexture("Projectiles/v08/PlantCloud");
			int spriteDirection = base.projectile.spriteDirection;
			Vector2 drawCenter2 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y);
			int num215 = texture.Height / 7;
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
	}
}
