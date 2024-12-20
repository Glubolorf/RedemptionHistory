using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class GreatGust : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/EaglecrestGolem/UkkoGust";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gust");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 82;
			base.projectile.height = 82;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 254;
			base.projectile.timeLeft = 100;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = false;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 1.02f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y * 1.02f;
			if (base.projectile.localAI[0] == 0f)
			{
				base.projectile.alpha -= 4;
			}
			else
			{
				base.projectile.localAI[0] += 1f;
			}
			if (base.projectile.localAI[0] >= 60f)
			{
				base.projectile.alpha += 2;
			}
			if (base.projectile.alpha < 180)
			{
				base.projectile.localAI[0] = 1f;
			}
			if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
			{
				base.projectile.spriteDirection = -base.projectile.direction;
			}
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (!this.clearCheck.noKnockback && base.projectile.alpha < 250 && Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.velocity.X = base.projectile.velocity.X / 2f;
					this.clearCheck.velocity.Y = base.projectile.velocity.Y / 2f;
				}
			}
			for (int p2 = 0; p2 < Main.npc.Length; p2++)
			{
				this.clearCheck2 = Main.npc[p2];
				if (!this.clearCheck2.friendly && base.projectile.alpha < 250 && Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck2.position, this.clearCheck2.Size))
				{
					this.clearCheck2.velocity.X = base.projectile.velocity.X * this.clearCheck2.knockBackResist;
					this.clearCheck2.velocity.Y = base.projectile.velocity.Y * this.clearCheck2.knockBackResist;
				}
			}
			if ((base.projectile.alpha >= 255 && base.projectile.localAI[0] != 0f) || (base.projectile.velocity.X == 0f && base.projectile.velocity.Y == 0f))
			{
				base.projectile.Kill();
			}
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

		private Player clearCheck;

		private NPC clearCheck2;
	}
}
