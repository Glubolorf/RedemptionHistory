using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class AtomNucleus : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Atom Nucleus");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			this.electronCounter++;
			if (this.electronCounter > 3)
			{
				this.electronFrame++;
				this.electronCounter = 0;
			}
			if (this.electronFrame >= 4)
			{
				this.electronFrame = 0;
			}
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 100f;
			bool target = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5 && !Main.npc[i].immortal)
				{
					Vector2 newMove = Main.npc[i].Center - base.projectile.Center;
					float distanceTo = (float)Math.Sqrt((double)(newMove.X * newMove.X + newMove.Y * newMove.Y));
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				this.AdjustMagnitude(ref move);
				base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 20f)
			{
				vector *= 20f / magnitude;
			}
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-10, 10), ModContent.ProjectileType<AtomNucleus2>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-10, 10), ModContent.ProjectileType<AtomNucleus2>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<DamagePro6>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 5f);
				Main.dust[dustIndex].velocity *= 3.4f;
			}
			for (int j = 0; j < 30; j++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex2].noGravity = true;
				Main.dust[dustIndex2].velocity *= 9f;
				dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex2].velocity *= 6f;
			}
			for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(base.projectile.position.X + (float)(base.projectile.width / 2) - 24f, base.projectile.position.Y + (float)(base.projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D electronAni = base.mod.GetTexture("Projectiles/Magic/AtomElectron");
			int spriteDirection = base.projectile.spriteDirection;
			Vector2 drawCenter2 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y);
			int num215 = texture.Height;
			int y7 = num215 * base.projectile.frame;
			Main.spriteBatch.Draw(texture, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), drawColor, base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Vector2 drawCenter3 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y);
			int num216 = electronAni.Height / 4;
			int y8 = num216 * this.electronFrame;
			Main.spriteBatch.Draw(electronAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, electronAni.Width, num216)), drawColor, 0f, new Vector2((float)electronAni.Width / 2f, (float)num216 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Main.spriteBatch.Draw(electronAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, electronAni.Width, num216)), drawColor, 1.5708f, new Vector2((float)electronAni.Width / 2f, (float)num216 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Main.spriteBatch.Draw(electronAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, electronAni.Width, num216)), drawColor, 3.92699f, new Vector2((float)electronAni.Width / 2f, (float)num216 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public int electronFrame;

		public int electronCounter;
	}
}
