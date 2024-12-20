using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave.Guardians
{
	public class NatureGuardian11 : Guardian
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mud Guardian");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 56;
			base.projectile.height = 72;
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
			this.BuffID = ModContent.BuffType<NatureGuardian11Buff>();
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 38, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override void BirthEffects(Player player)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 38, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<MudAura>(), 0, 0f, player.whoAmI, 0f, 0f);
		}

		public override void ActiveUpdate(Player player)
		{
			if (base.projectile.localAI[0] % 160f == 0f)
			{
				if (Main.rand.Next(3) == 0)
				{
					if (base.projectile.spriteDirection == -1)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 26f, base.projectile.position.Y + 32f), new Vector2((float)(-6 + Main.rand.Next(-6, 0)), (float)(-4 + Main.rand.Next(-4, 0))), ModContent.ProjectileType<MudGloopPro>(), 20, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 26f, base.projectile.position.Y + 32f), new Vector2((float)(-6 + Main.rand.Next(-6, 0)), (float)(-4 + Main.rand.Next(-4, 0))), ModContent.ProjectileType<MudGloopPro>(), 20, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 26f, base.projectile.position.Y + 32f), new Vector2((float)(-6 + Main.rand.Next(-6, 0)), (float)(-4 + Main.rand.Next(-4, 0))), ModContent.ProjectileType<MudGloopPro>(), 20, 3f, Main.myPlayer, 0f, 0f);
					}
					else
					{
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 32f, base.projectile.position.Y + 32f), new Vector2((float)(6 + Main.rand.Next(0, 6)), (float)(-4 + Main.rand.Next(-4, 0))), ModContent.ProjectileType<MudGloopPro>(), 20, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 32f, base.projectile.position.Y + 32f), new Vector2((float)(6 + Main.rand.Next(0, 6)), (float)(-4 + Main.rand.Next(-4, 0))), ModContent.ProjectileType<MudGloopPro>(), 20, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 32f, base.projectile.position.Y + 32f), new Vector2((float)(6 + Main.rand.Next(0, 6)), (float)(-4 + Main.rand.Next(-4, 0))), ModContent.ProjectileType<MudGloopPro>(), 20, 3f, Main.myPlayer, 0f, 0f);
					}
				}
				if (base.projectile.spriteDirection == -1)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 26f, base.projectile.position.Y + 32f), new Vector2(-10f, 0f), ModContent.ProjectileType<MudGloopPro>(), 20, 3f, Main.myPlayer, 0f, 0f);
					return;
				}
				Projectile.NewProjectile(new Vector2(base.projectile.position.X + 32f, base.projectile.position.Y + 32f), new Vector2(10f, 0f), ModContent.ProjectileType<MudGloopPro>(), 20, 3f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}
