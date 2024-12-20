using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Stave.Guardians
{
	public class NatureGuardian8 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupt Fairy");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 28;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 80;
			base.projectile.timeLeft = 36000;
			base.projectile.netImportant = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			if (!player.HasBuff(ModContent.BuffType<NatureGuardian8Buff>()))
			{
				base.projectile.Kill();
			}
			if (player.direction == 1)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 1f)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				Main.PlaySound(SoundID.Item74, base.projectile.position);
			}
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.position.X = player.Center.X - 12f;
			base.projectile.position.Y = player.Center.Y - 64f;
			if ((RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC) && this.shootBarrage) ? (base.projectile.localAI[0] % 60f == 0f) : (base.projectile.localAI[0] % 180f == 0f))
			{
				if (Main.rand.Next(5) == 0 && !this.shootBarrage)
				{
					this.shootBarrage = true;
				}
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<CorruptBlast>(), 20, 7f, Main.myPlayer, 0f, 0f);
			}
			if (this.shootBarrage && base.projectile.localAI[0] % 180f == 0f)
			{
				this.shootBarrage = false;
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.natureGuardian8 = false;
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		private bool shootBarrage;

		private NPC target;
	}
}
