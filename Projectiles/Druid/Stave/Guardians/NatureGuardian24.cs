using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave.Guardians
{
	public class NatureGuardian24 : Guardian
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spore Pixie");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 14;
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
			this.BuffID = ModContent.BuffType<NatureGuardian24Buff>();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 44, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void BirthEffects(Player player)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 44, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void ActiveUpdate(Player player)
		{
			if (base.projectile.localAI[0] % 15f == 0f)
			{
				int p = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-12 + Main.rand.Next(0, 24)), (float)(-12 + Main.rand.Next(0, 24)), 567, 11, 3f, Main.myPlayer, 0f, 0f);
				Main.projectile[p].GetGlobalProjectile<DruidProjectile>().druidic = true;
				Main.projectile[p].GetGlobalProjectile<DruidProjectile>().fromStave = true;
				Main.projectile[p].magic = false;
				Main.projectile[p].melee = false;
				Main.projectile[p].ranged = false;
			}
		}
	}
}
