using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave.Guardians
{
	public class NatureGuardian28 : Guardian
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moon Baron");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 42;
			base.projectile.height = 46;
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
			this.BuffID = ModContent.BuffType<NatureGuardian28Buff>();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 45; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 229, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void BirthEffects(Player player)
		{
			for (int i = 0; i < 45; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 229, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void ActiveUpdate(Player player)
		{
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC, null))
			{
				base.projectile.localAI[1] += 1f;
				if (base.projectile.localAI[1] % 2f == 0f && base.projectile.localAI[1] < 30f)
				{
					int p = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(16f, Utils.ToRotation(this.target.Center - base.projectile.Center)), 462, 50, 2f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].hostile = false;
					Main.projectile[p].friendly = true;
					Main.projectile[p].timeLeft = 60;
					Main.projectile[p].netUpdate = true;
				}
				if (base.projectile.localAI[1] >= 120f)
				{
					base.projectile.localAI[1] = 0f;
				}
			}
		}

		private NPC target;
	}
}
