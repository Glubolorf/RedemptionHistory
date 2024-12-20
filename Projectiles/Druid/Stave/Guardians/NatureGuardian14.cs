using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave.Guardians
{
	public class NatureGuardian14 : Guardian
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lunar Statuette");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 42;
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
			this.BuffID = ModContent.BuffType<NatureGuardian14Buff>();
		}

		public override void BirthEffects(Player player)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 21, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void ActiveUpdate(Player player)
		{
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[0] % 60f == 0f)
			{
				if (Main.rand.Next(10) == 0)
				{
					Main.PlaySound(SoundID.Item105, (int)base.projectile.position.X, (int)base.projectile.position.Y);
					for (int i = -4; i <= 4; i++)
					{
						int p = Projectile.NewProjectile(base.projectile.Center, 10f * Utils.RotatedBy(Vector2.UnitX, 0.7853981633974483 * (double)i, default(Vector2)), 503, 25, 7f, Main.myPlayer, 0f, 0f);
						Main.projectile[p].GetGlobalProjectile<DruidProjectile>().druidic = true;
						Main.projectile[p].GetGlobalProjectile<DruidProjectile>().fromStave = true;
						Main.projectile[p].melee = false;
					}
				}
				if (Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) + 25f), ModContent.ProjectileType<NightSoulPro2>(), 15, 3f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) - 25f), ModContent.ProjectileType<NightSoulPro2>(), 15, 3f, Main.myPlayer, 0f, 0f);
				}
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<NightSoulPro2>(), 15, 3f, Main.myPlayer, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 21, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		private NPC target;
	}
}
