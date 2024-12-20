using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.VCleaver
{
	public class CleaverClone2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/VCleaver/CleaverClone1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Phantom Cleaver");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 98;
			base.projectile.height = 280;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.5f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.rotation = 3.1415927f;
			float obj = base.projectile.localAI[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					return;
				}
				base.projectile.hostile = true;
				if (base.projectile.localAI[1] == 0f)
				{
					base.projectile.velocity.Y = 20f;
					base.projectile.localAI[1] = 1f;
				}
				base.projectile.alpha += 3;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			else
			{
				base.projectile.alpha -= 5;
				base.projectile.velocity *= 0.97f;
				if (base.projectile.velocity.Length() < 1f && base.projectile.alpha <= 0)
				{
					base.projectile.velocity *= 0f;
					base.projectile.localAI[0] = 1f;
					return;
				}
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(ModContent.BuffType<SnippedDebuff>(), Main.expertMode ? 1200 : 600, true);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float collisionPoint = 0f;
			return new bool?(Collision.CheckAABBvLineCollision(Utils.Center(targetHitbox), Utils.Size(targetHitbox), base.projectile.Center, base.projectile.Center + Utils.ToRotationVector2(base.projectile.rotation + -1.5707964f) * 140f, 58f, ref collisionPoint));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(Color.Red) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public float rot;
	}
}
