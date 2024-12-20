using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class SpiritWyvernProCrimson : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crimson Spirit Wyvern");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 18;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.alpha = 60;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = false;
		}

		public override void AI()
		{
			if (Main.player[base.projectile.owner].Center.X > base.projectile.Center.X)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 5)
				{
					base.projectile.frame = 0;
				}
			}
			Projectile projectile3 = base.projectile;
			projectile3.velocity.X = projectile3.velocity.X * 0f;
			Projectile projectile4 = base.projectile;
			projectile4.velocity.Y = projectile4.velocity.Y * 0f;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha += 2;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			if (base.projectile.localAI[0] % 25f == 0f)
			{
				Main.PlaySound(SoundID.NPCDeath8.WithVolume(0.2f), base.projectile.position);
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 0)
				{
					if (base.projectile.spriteDirection == 1)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 36f), new Vector2(-6f, 0f), base.mod.ProjectileType("SpiritFlameCrimson"), base.projectile.damage, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
					}
					else
					{
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 60f, base.projectile.position.Y + 36f), new Vector2(6f, 0f), base.mod.ProjectileType("SpiritFlameCrimson"), base.projectile.damage, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
					}
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 1)
				{
					int numberProjectiles = 2;
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = (base.projectile.spriteDirection == 1) ? Utils.RotatedByRandom(new Vector2(-6f, 0f), (double)MathHelper.ToRadians(15f)) : Utils.RotatedByRandom(new Vector2(6f, 0f), (double)MathHelper.ToRadians(15f));
						float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
						perturbedSpeed *= scale;
						if (base.projectile.spriteDirection == 1)
						{
							Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 36f, perturbedSpeed.X, perturbedSpeed.Y, base.mod.ProjectileType("SpiritFlameCrimson"), base.projectile.damage, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
						}
						else
						{
							Projectile.NewProjectile(base.projectile.position.X + 60f, base.projectile.position.Y + 36f, perturbedSpeed.X, perturbedSpeed.Y, base.mod.ProjectileType("SpiritFlameCrimson"), base.projectile.damage, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
						}
					}
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras == 2)
				{
					int numberProjectiles2 = 3;
					for (int j = 0; j < numberProjectiles2; j++)
					{
						Vector2 perturbedSpeed2 = (base.projectile.spriteDirection == 1) ? Utils.RotatedByRandom(new Vector2(-6f, 0f), (double)MathHelper.ToRadians(25f)) : Utils.RotatedByRandom(new Vector2(6f, 0f), (double)MathHelper.ToRadians(25f));
						float scale2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
						perturbedSpeed2 *= scale2;
						if (base.projectile.spriteDirection == 1)
						{
							Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 36f, perturbedSpeed2.X, perturbedSpeed2.Y, base.mod.ProjectileType("SpiritFlameCrimson"), base.projectile.damage, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
						}
						else
						{
							Projectile.NewProjectile(base.projectile.position.X + 60f, base.projectile.position.Y + 36f, perturbedSpeed2.X, perturbedSpeed2.Y, base.mod.ProjectileType("SpiritFlameCrimson"), base.projectile.damage, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
						}
					}
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritExtras >= 3)
				{
					int numberProjectiles3 = 4;
					for (int k = 0; k < numberProjectiles3; k++)
					{
						Vector2 perturbedSpeed3 = (base.projectile.spriteDirection == 1) ? Utils.RotatedByRandom(new Vector2(-6f, 0f), (double)MathHelper.ToRadians(35f)) : Utils.RotatedByRandom(new Vector2(6f, 0f), (double)MathHelper.ToRadians(35f));
						float scale3 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
						perturbedSpeed3 *= scale3;
						if (base.projectile.spriteDirection == 1)
						{
							Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 36f, perturbedSpeed3.X, perturbedSpeed3.Y, base.mod.ProjectileType("SpiritFlameCrimson"), base.projectile.damage, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
						}
						else
						{
							Projectile.NewProjectile(base.projectile.position.X + 60f, base.projectile.position.Y + 36f, perturbedSpeed3.X, perturbedSpeed3.Y, base.mod.ProjectileType("SpiritFlameCrimson"), base.projectile.damage, (float)((int)base.projectile.knockBack), base.projectile.owner, 0f, 1f);
						}
					}
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath6.WithVolume(0.4f), base.projectile.position);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 90, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 20, default(Color), 1f);
		}
	}
}
