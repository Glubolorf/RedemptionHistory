using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class RingStavePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/Druid/Stave/" + base.GetType().Name);
				RingStavePro.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Hallowed Ring");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 5;
			base.projectile.tileCollide = true;
			base.projectile.timeLeft = 120;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
			base.projectile.glowMask = RingStavePro.customGlowMask;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			for (int i = 0; i < 8; i++)
			{
				double angle = (double)i * 0.7853981633974483;
				this.vector.X = (float)(Math.Sin(angle) * 11.0);
				this.vector.Y = (float)(Math.Cos(angle) * 11.0);
				Dust dust = Main.dust[Dust.NewDust(base.projectile.Center + this.vector - new Vector2(4f, 4f), 1, 1, 57, 0f, 0f, 100, default(Color), 1f)];
				dust.noGravity = true;
				dust.velocity *= 0f;
			}
			float[] localAI = base.projectile.localAI;
			int num = 0;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 % 20f == 0f)
			{
				base.projectile.localAI[1] += 1f;
				if (base.projectile.localAI[1] == 1f)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<RingStaveProSight>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
					return;
				}
				if (base.projectile.localAI[1] == 2f)
				{
					Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<RingStaveProMight>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
					return;
				}
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<RingStaveProFright>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
				base.projectile.localAI[1] = 0f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Item110.WithVolume(0.2f), base.projectile.position);
			if (base.projectile.velocity.X != oldVelocity.X)
			{
				base.projectile.velocity.X = -oldVelocity.X;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y)
			{
				base.projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item110, base.projectile.position);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(lightColor) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public static short customGlowMask;

		private Vector2 vector;
	}
}
