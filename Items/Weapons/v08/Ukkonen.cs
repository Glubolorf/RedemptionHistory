using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class Ukkonen : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ukkonen");
			Main.projFrames[base.projectile.type] = 16;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 74;
			base.projectile.height = 36;
			base.projectile.penetrate = -1;
			base.projectile.minion = true;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.netImportant = true;
			base.projectile.timeLeft = 18000;
			base.projectile.minionSlots = 1f;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 4)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 16)
				{
					base.projectile.frame = 0;
				}
			}
			bool flag = base.projectile.type == base.mod.ProjectileType("Ukkonen");
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (flag)
			{
				if (player.dead)
				{
					modPlayer.ukkonenMinion = false;
				}
				if (modPlayer.ukkonenMinion)
				{
					base.projectile.timeLeft = 2;
				}
			}
			if (player.dead || !player.HasBuff(base.mod.BuffType("UkkonenBuff")))
			{
				base.projectile.Kill();
			}
			Vector2 DefaultPos = new Vector2(player.Center.X, player.Center.Y - 80f);
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, true, player.MinionAttackTargetNPC))
			{
				Vector2 AttackPos = new Vector2(this.target.Center.X, this.target.position.Y - 60f);
				this.MoveToVector2(AttackPos);
				if (Main.rand.Next(5) == 0)
				{
					int p = Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-36, 36), base.projectile.Center.Y + (float)Main.rand.Next(-12, 12)), new Vector2(0f, 0f), base.mod.ProjectileType("UkkoRain"), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
					Main.projectile[p].friendly = true;
					Main.projectile[p].hostile = false;
				}
				if (Main.rand.Next(100) == 0)
				{
					Vector2 speed = Vector2.Normalize(Utils.RotatedBy(new Vector2(1f, 0f), (double)(base.projectile.rotation + 3.1415f), default(Vector2)));
					speed = (float)((Main.rand.Next(2) == 0) ? 1 : -1) * speed;
					float ai = (float)Main.rand.Next(120);
					int p2 = Projectile.NewProjectile(base.projectile.Center.X + (float)Main.rand.Next(-20, 20), base.projectile.Center.Y, 0f, 10f, base.mod.ProjectileType("UkkoLightning"), base.projectile.damage, 0f, Main.myPlayer, Utils.ToRotation(speed) + 1000f, ai);
					Main.projectile[p2].hostile = false;
					Main.projectile[p2].friendly = true;
					return;
				}
			}
			else
			{
				this.MoveToVector3(DefaultPos);
			}
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = 30f;
			float velMultiplier = 1f;
			Vector2 dist = p - base.projectile.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.projectile.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.projectile.velocity *= moveSpeed;
			base.projectile.velocity *= velMultiplier;
		}

		public void MoveToVector3(Vector2 p)
		{
			float moveSpeed = 14f;
			float velMultiplier = 1f;
			Vector2 dist = p - base.projectile.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.projectile.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.projectile.velocity *= moveSpeed;
			base.projectile.velocity *= velMultiplier;
		}

		public override bool MinionContactDamage()
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 15; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 269, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		private NPC target;
	}
}
