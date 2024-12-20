using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class ShadeNote2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Song of the Abyss");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 24;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = 1;
			base.projectile.magic = true;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			int npc = (int)base.projectile.ai[0];
			NPC target = Main.npc[(int)base.projectile.ai[0]];
			if (npc < 0 || npc >= 200 || !Main.npc[npc].active)
			{
				base.projectile.Kill();
			}
			base.projectile.rotation = base.projectile.velocity.X * 0.05f;
			if (base.projectile.timeLeft > 150)
			{
				base.projectile.velocity *= 0.94f;
				return;
			}
			base.projectile.friendly = true;
			this.Move(target.Center, (float)((base.projectile.timeLeft > 50) ? 30 : 50), 20f);
		}

		public void Move(Vector2 offset, float speed, float turnResistance = 10f)
		{
			Vector2 move = offset - base.projectile.Center;
			float magnitude = ShadeNote2.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			move = (base.projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = ShadeNote2.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			base.projectile.velocity = move;
		}

		public static float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
			{
				Color.Indigo,
				Color.GhostWhite,
				Color.Indigo
			}));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1f;
				Main.dust[dustIndex].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 120, false);
		}
	}
}
