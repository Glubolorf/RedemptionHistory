using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Dusts;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave.Guardians
{
	public class NatureGuardian6 : Guardian
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moonflare");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
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
			this.BuffID = ModContent.BuffType<NatureGuardian6Buff>();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<MoonflareDust>(), 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void BirthEffects(Player player)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<MoonflareDust>(), 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void ActiveUpdate(Player player)
		{
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[0] % 40f == 0f)
			{
				if (Main.rand.Next(5) == 0)
				{
					int p = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(15f, Utils.ToRotation(this.target.Center - base.projectile.Center) + 25f), ModContent.ProjectileType<LunarShotPro>(), 7, 3f, Main.myPlayer, 0f, 0f);
					p = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(15f, Utils.ToRotation(this.target.Center - base.projectile.Center) - 25f), ModContent.ProjectileType<LunarShotPro>(), 7, 3f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].ranged = false;
					Main.projectile[p].GetGlobalProjectile<DruidProjectile>().druidic = true;
					Main.projectile[p].GetGlobalProjectile<DruidProjectile>().fromStave = true;
				}
				if (Main.rand.Next(5) == 0)
				{
					int p2 = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<LunarShotPro>(), 7, 3f, Main.myPlayer, 0f, 0f);
					p2 = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(5f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<LunarShotPro>(), 7, 3f, Main.myPlayer, 0f, 0f);
					Main.projectile[p2].ranged = false;
					Main.projectile[p2].GetGlobalProjectile<DruidProjectile>().druidic = true;
					Main.projectile[p2].GetGlobalProjectile<DruidProjectile>().fromStave = true;
				}
				int p3 = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(15f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<LunarShotPro>(), 7, 3f, Main.myPlayer, 0f, 0f);
				Main.projectile[p3].ranged = false;
				Main.projectile[p3].GetGlobalProjectile<DruidProjectile>().druidic = true;
				Main.projectile[p3].GetGlobalProjectile<DruidProjectile>().fromStave = true;
			}
		}

		private NPC target;
	}
}
