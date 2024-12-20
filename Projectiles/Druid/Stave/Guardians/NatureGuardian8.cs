using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave.Guardians
{
	public class NatureGuardian8 : Guardian
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
			this.BuffID = ModContent.BuffType<NatureGuardian8Buff>();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void BirthEffects(Player player)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void ActiveUpdate(Player player)
		{
			if ((RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && this.shootBarrage) ? (base.projectile.localAI[0] % 60f == 0f) : (base.projectile.localAI[0] % 180f == 0f))
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
		}

		private bool shootBarrage;

		private NPC target;
	}
}
