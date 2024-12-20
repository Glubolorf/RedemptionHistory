using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class EchoF : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/Melee/EchoF";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Echo");
			Main.projFrames[base.projectile.type] = 10;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 32;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = 1;
			base.projectile.melee = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 10)
				{
					base.projectile.frame = 0;
				}
			}
			if (base.projectile.velocity.X > 0f)
			{
				base.projectile.spriteDirection = -1;
			}
			else
			{
				base.projectile.spriteDirection = 1;
			}
			base.projectile.rotation = base.projectile.velocity.X * 0.05f;
			if (base.projectile.timeLeft > 120)
			{
				base.projectile.velocity *= 0.98f;
				return;
			}
			base.projectile.friendly = true;
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, true, player.MinionAttackTargetNPC, null))
			{
				this.Move(this.target.Center, (float)((base.projectile.timeLeft > 60) ? 15 : 20), 20f);
				return;
			}
			base.projectile.velocity *= 0.98f;
		}

		public void Move(Vector2 offset, float speed, float turnResistance = 10f)
		{
			Vector2 move = offset - base.projectile.Center;
			float magnitude = EchoF.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			move = (base.projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = EchoF.Magnitude(move);
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
				Color.GhostWhite,
				Color.Black,
				Color.GhostWhite
			}));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1f;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 120, false);
		}

		private NPC target;
	}
}
