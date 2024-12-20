using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	public class KeeperBloodWave : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blood Wave");
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 48;
			base.projectile.height = 48;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, base.projectile.Opacity * 0.5f, base.projectile.Opacity * 0.2f, base.projectile.Opacity * 0.2f);
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + 1.57f;
			base.projectile.alpha += 5;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
		}

		public override bool CanHitPlayer(Player target)
		{
			return base.projectile.alpha < 200;
		}

		public override bool? CanHitNPC(NPC target)
		{
			return new bool?(target.friendly && base.projectile.alpha < 200);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Width);
			Vector2 drawOrigin = new Vector2((float)(texture.Width / 2), (float)(base.projectile.height / 2));
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = Color.White * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(texture, drawPos, new Rectangle?(rect), base.projectile.GetAlpha(color), base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, new Rectangle?(rect), base.projectile.GetAlpha(lightColor), base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(30, 260, true);
			NPC host = Main.npc[(int)base.projectile.ai[0]];
			if (host.life < host.lifeMax - 60)
			{
				int steps = (int)host.Distance(target.Center) / 8;
				for (int i = 0; i < steps; i++)
				{
					if (Utils.NextBool(Main.rand, 2))
					{
						Dust dust = Dust.NewDustDirect(Vector2.Lerp(host.Center, target.Center, (float)i / (float)steps), 2, 2, 235, 0f, 0f, 0, default(Color), 2f);
						dust.velocity = target.DirectionTo(dust.position) * 2f;
						dust.noGravity = true;
					}
				}
				host.life += 60;
				host.HealEffect(60, true);
			}
		}
	}
}
