using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class ShadeTreble : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Song of the Abyss");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 50;
			base.projectile.magic = true;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 20;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			base.projectile.velocity *= 0.98f;
			base.projectile.rotation = base.projectile.velocity.X * 0.05f;
			if (base.projectile.localAI[0] == 0f)
			{
				if (Main.myPlayer == base.projectile.owner)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
					{
						int degrees = 0;
						for (int i = 0; i < 2; i++)
						{
							degrees += 180;
							int p = Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<SongDust>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p].ai[0] = (float)base.projectile.whoAmI;
							Main.projectile[p].ai[1] = (float)degrees;
						}
						break;
					}
					case 1:
					{
						int degrees2 = 0;
						for (int j = 0; j < 4; j++)
						{
							degrees2 += 90;
							int p2 = Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<SongDust>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p2].ai[0] = (float)base.projectile.whoAmI;
							Main.projectile[p2].ai[1] = (float)degrees2;
						}
						break;
					}
					case 2:
					{
						int degrees3 = 0;
						for (int k = 0; k < 6; k++)
						{
							degrees3 += 60;
							int p3 = Projectile.NewProjectile(base.projectile.Center, Vector2.Zero, ModContent.ProjectileType<SongDust>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[p3].ai[0] = (float)base.projectile.whoAmI;
							Main.projectile[p3].ai[1] = (float)degrees3;
						}
						break;
					}
					}
				}
				base.projectile.localAI[0] = 1f;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
			{
				Color.GhostWhite,
				Color.Black,
				Color.GhostWhite,
				Color.DarkSlateBlue,
				Color.GhostWhite,
				Color.Indigo,
				Color.GhostWhite
			}));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1f;
				Main.dust[dustIndex].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			float obj = base.projectile.ai[0];
			if (!0f.Equals(obj))
			{
				if (1f.Equals(obj))
				{
					if (Main.myPlayer == base.projectile.owner)
					{
						Projectile.NewProjectile(base.projectile.Center, new Vector2(14f, 0f), ModContent.ProjectileType<ShadeNote2>(), base.projectile.damage / 2, base.projectile.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
						Projectile.NewProjectile(base.projectile.Center, new Vector2(-14f, 0f), ModContent.ProjectileType<ShadeNote2>(), base.projectile.damage / 2, base.projectile.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
						Projectile.NewProjectile(base.projectile.Center, new Vector2(0f, -14f), ModContent.ProjectileType<ShadeNote2>(), base.projectile.damage / 2, base.projectile.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
						Projectile.NewProjectile(base.projectile.Center, new Vector2(0f, 14f), ModContent.ProjectileType<ShadeNote2>(), base.projectile.damage / 2, base.projectile.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
					}
				}
			}
			else if (Main.myPlayer == base.projectile.owner)
			{
				Projectile.NewProjectile(base.projectile.Center, new Vector2(7f, 7f), ModContent.ProjectileType<ShadeNote1>(), base.projectile.damage / 2, base.projectile.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
				Projectile.NewProjectile(base.projectile.Center, new Vector2(-7f, -7f), ModContent.ProjectileType<ShadeNote1>(), base.projectile.damage / 2, base.projectile.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
				Projectile.NewProjectile(base.projectile.Center, new Vector2(7f, -7f), ModContent.ProjectileType<ShadeNote1>(), base.projectile.damage / 2, base.projectile.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
				Projectile.NewProjectile(base.projectile.Center, new Vector2(-7f, 7f), ModContent.ProjectileType<ShadeNote1>(), base.projectile.damage / 2, base.projectile.knockBack, Main.myPlayer, (float)target.whoAmI, 0f);
			}
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 120, false);
		}
	}
}
