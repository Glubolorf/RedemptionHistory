using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class VesselStaffPro2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Ring");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 100;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.magic = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 600;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.dead || !player.active || modPlayer.shadowBinderCharge <= 0)
			{
				base.projectile.Kill();
			}
			base.projectile.velocity *= 0f;
			base.projectile.Center = player.Center;
			if (base.projectile.ai[0] == 0f)
			{
				base.projectile.ai[0] = 100f;
			}
			else
			{
				base.projectile.ai[0] += 1f;
				if (base.projectile.ai[0] % 10f == 0f)
				{
					modPlayer.shadowBinderCharge--;
				}
			}
			for (int i = 0; i < 3; i++)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * (double)base.projectile.ai[0]);
				this.vector.Y = (float)(Math.Cos(angle) * (double)base.projectile.ai[0]);
				Dust dust2 = Main.dust[Dust.NewDust(player.Center + this.vector, 2, 2, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 3f)];
				dust2.noGravity = true;
				dust2.velocity = -player.DirectionTo(dust2.position) * 4f;
			}
			for (int p = 0; p < 200; p++)
			{
				NPC npc = Main.npc[p];
				if (!npc.immortal && !npc.dontTakeDamage && !npc.friendly && base.projectile.Distance(npc.Center) < base.projectile.ai[0] && base.projectile.ai[0] % 5f == 0f)
				{
					player.ApplyDamageToNPC(npc, 400, 0f, 0, false);
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(Color.White) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		private Vector2 vector;
	}
}
