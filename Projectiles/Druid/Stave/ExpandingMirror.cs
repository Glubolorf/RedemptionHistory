using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class ExpandingMirror : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Expanding Mirror");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 8;
			base.projectile.tileCollide = false;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 120;
			base.projectile.extraUpdates = 2;
			base.projectile.usesLocalNPCImmunity = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
			base.projectile.penetrate = -1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 0;
			base.projectile.localNPCImmunity[target.whoAmI] = -1;
		}

		public override void AI()
		{
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity);
			if (base.projectile.timeLeft < this.fadeAwayTime)
			{
				base.projectile.alpha += 255 / this.fadeAwayTime;
			}
			else if (base.projectile.alpha > 0)
			{
				base.projectile.alpha -= 255 / this.fadeInTime;
			}
			else
			{
				base.projectile.alpha = 0;
			}
			base.projectile.frameCounter++;
			if (base.projectile.frameCounter % 5 == 0)
			{
				this.middleSegments++;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			float length = (float)(this.middleSegments * 2 + base.projectile.height * 2);
			Vector2 offLocation = base.projectile.Center + ExpandingMirror.PolarVector(length / 2f, base.projectile.rotation - 1.5707964f);
			spriteBatch.Draw(texture, offLocation - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, base.projectile.width, base.projectile.height)), Color.Lerp(lightColor, new Color(0, 0, 0, 0), (float)base.projectile.alpha / 255f), base.projectile.rotation, new Vector2((float)base.projectile.width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
			offLocation += ExpandingMirror.PolarVector((float)base.projectile.height, base.projectile.rotation + 1.5707964f);
			for (int i = 0; i < this.middleSegments; i++)
			{
				spriteBatch.Draw(texture, offLocation - Main.screenPosition, new Rectangle?(new Rectangle(0, base.projectile.height, base.projectile.width, 2)), Color.Lerp(lightColor, new Color(0, 0, 0, 0), (float)base.projectile.alpha / 255f), base.projectile.rotation, new Vector2((float)base.projectile.width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
				offLocation += ExpandingMirror.PolarVector(2f, base.projectile.rotation + 1.5707964f);
			}
			spriteBatch.Draw(texture, offLocation - Main.screenPosition, new Rectangle?(new Rectangle(0, base.projectile.height + 2, base.projectile.width, base.projectile.height)), Color.Lerp(lightColor, new Color(0, 0, 0, 0), (float)base.projectile.alpha / 255f), base.projectile.rotation, new Vector2((float)base.projectile.width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
			return false;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float length = (float)(this.middleSegments * 2 + base.projectile.height * 2);
			Vector2 offLocation = base.projectile.Center + ExpandingMirror.PolarVector(length / 2f, base.projectile.rotation - 1.5707964f);
			float point = 0f;
			return new bool?(Collision.CheckAABBvLineCollision(Utils.ToVector2(targetHitbox.Location), Utils.Size(targetHitbox), offLocation, offLocation + ExpandingMirror.PolarVector(length, base.projectile.rotation + 1.5707964f), (float)base.projectile.width, ref point));
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.type == 480)
			{
				damage *= 200;
			}
		}

		private static Vector2 PolarVector(float radius, float theta)
		{
			return new Vector2((float)Math.Cos((double)theta), (float)Math.Sin((double)theta)) * radius;
		}

		private int middleSegments = 1;

		private int fadeAwayTime = 30;

		private int fadeInTime = 30;
	}
}
