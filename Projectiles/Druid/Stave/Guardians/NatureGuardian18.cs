using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave.Guardians
{
	public class NatureGuardian18 : Guardian
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tree of Creation");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 154;
			base.projectile.height = 146;
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
			this.BuffID = ModContent.BuffType<NatureGuardian18Buff>();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 45; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void BirthEffects(Player player)
		{
			for (int i = 0; i < 45; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void ActiveUpdate(Player player)
		{
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[0] % 80f == 0f)
			{
				if (Main.rand.Next(10) == 0)
				{
					Main.PlaySound(SoundID.Item105, (int)base.projectile.position.X, (int)base.projectile.position.Y);
					for (int i = -4; i <= 4; i++)
					{
						Projectile.NewProjectile(base.projectile.Center, 12f * Utils.RotatedBy(Vector2.UnitX, 0.7853981633974483 * (double)i, default(Vector2)), ModContent.ProjectileType<HerbOfLifePro>(), 40, 3f, Main.myPlayer, 0f, 0f);
					}
					for (int j = -4; j <= 4; j++)
					{
						Projectile.NewProjectile(base.projectile.Center, 8f * Utils.RotatedBy(Vector2.UnitX, 0.7853981633974483 * (double)j, default(Vector2)), ModContent.ProjectileType<HerbOfLifePro>(), 40, 3f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Main.rand.Next(5) == 0)
				{
					for (int k = 0; k < 6; k++)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-12 + Main.rand.Next(0, 24)), (float)(-12 + Main.rand.Next(0, 24)), ModContent.ProjectileType<CreationRosePetal>(), 40, 3f, Main.myPlayer, 0f, 0f);
					}
				}
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2((float)(-12 + Main.rand.Next(0, 24)), (float)(-12 + Main.rand.Next(0, 24))), ModContent.ProjectileType<CreationRosePetal>(), 40, 3f, Main.myPlayer, 0f, 0f);
			}
		}

		private NPC target;
	}
}
