using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.Thorn;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class UkkoRainCloud : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rain Cloud");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 354;
			base.projectile.height = 144;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 600;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 3f)
			{
				base.projectile.frame = 1;
			}
			else if (base.projectile.localAI[0] == 4f)
			{
				base.projectile.frame = 2;
			}
			else
			{
				base.projectile.frame = 0;
			}
			if (base.projectile.alpha > 0 && base.projectile.timeLeft >= 60 && base.projectile.alpha > 100)
			{
				base.projectile.alpha -= 5;
			}
			if (base.projectile.timeLeft < 60)
			{
				base.projectile.alpha += 5;
			}
			if (base.projectile.alpha <= 100)
			{
				foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
				{
					if (base.projectile != proj && !proj.friendly && !proj.minion && base.projectile.localAI[0] == 0f)
					{
						if (proj.type == ModContent.ProjectileType<UkkoBlizzard>())
						{
							for (int i = 0; i < 20; i++)
							{
								Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 56, 0f, 0f, 100, default(Color), 1.5f);
								dust.velocity = -base.projectile.DirectionTo(dust.position);
							}
							base.projectile.localAI[0] = 1f;
						}
						else if (proj.type == ModContent.ProjectileType<DualcastBall>() || proj.type == ModContent.ProjectileType<UkkoLightning>() || proj.type == ModContent.ProjectileType<UkkoThunderwave>() || proj.type == ModContent.ProjectileType<UkkoStrike>())
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap1").WithVolume(0.3f).WithPitchVariance(0.1f), -1, -1);
							for (int j = 0; j < 20; j++)
							{
								Dust dust2 = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 226, 0f, 0f, 100, default(Color), 1.5f);
								dust2.velocity = -base.projectile.DirectionTo(dust2.position);
							}
							base.projectile.localAI[0] = 2f;
						}
						else if (proj.type == ModContent.ProjectileType<AkkaPoisonBubble>())
						{
							for (int k = 0; k < 20; k++)
							{
								Dust dust3 = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 70, 0f, 0f, 100, default(Color), 1.5f);
								dust3.velocity = -base.projectile.DirectionTo(dust3.position);
							}
							base.projectile.localAI[0] = 3f;
						}
						else if (proj.type == ModContent.ProjectileType<AkkaHealingSpirit>())
						{
							for (int l = 0; l < 20; l++)
							{
								Dust dust4 = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 99, 0f, 0f, 100, default(Color), 1.5f);
								dust4.velocity = -base.projectile.DirectionTo(dust4.position);
							}
							base.projectile.localAI[0] = 4f;
						}
					}
				}
				if (base.projectile.localAI[0] == 0f)
				{
					if (Main.rand.Next(5) == 0)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-150, 150), base.projectile.Center.Y + (float)Main.rand.Next(4, 8)), new Vector2(0f, 0f), ModContent.ProjectileType<UkkoRain>(), 0, 0f, base.projectile.owner, 0f, 0f);
						return;
					}
				}
				else if (base.projectile.localAI[0] == 1f)
				{
					if (Main.rand.Next(5) == 0)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-150, 150), base.projectile.Center.Y + (float)Main.rand.Next(4, 8)), new Vector2(0f, 0f), ModContent.ProjectileType<UkkoHail>(), 0, 0f, base.projectile.owner, 0f, 0f);
					}
					float[] localAI = base.projectile.localAI;
					int num = 1;
					float num2 = localAI[num] + 1f;
					localAI[num] = num2;
					if (num2 >= 180f)
					{
						base.projectile.localAI[1] = 0f;
						base.projectile.localAI[0] = 0f;
						return;
					}
				}
				else if (base.projectile.localAI[0] == 2f)
				{
					if (Main.rand.Next(3) == 0)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap2").WithVolume(0.2f).WithPitchVariance(0.1f), -1, -1);
						Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-150, 150), base.projectile.Center.Y + (float)Main.rand.Next(4, 8)), new Vector2(0f, 9f), ModContent.ProjectileType<ElectricZapPro1>(), 28, 0f, base.projectile.owner, 0f, 0f);
					}
					float[] localAI2 = base.projectile.localAI;
					int num3 = 1;
					float num2 = localAI2[num3] + 1f;
					localAI2[num3] = num2;
					if (num2 >= 50f)
					{
						base.projectile.localAI[1] = 0f;
						base.projectile.localAI[0] = 0f;
						return;
					}
				}
				else if (base.projectile.localAI[0] == 3f)
				{
					if (Main.rand.Next(5) == 0)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-150, 150), base.projectile.Center.Y + (float)Main.rand.Next(4, 8)), new Vector2(0f, 0f), ModContent.ProjectileType<UkkoRainPoison>(), 0, 0f, base.projectile.owner, 0f, 0f);
					}
					float[] localAI3 = base.projectile.localAI;
					int num4 = 1;
					float num2 = localAI3[num4] + 1f;
					localAI3[num4] = num2;
					if (num2 >= 180f)
					{
						base.projectile.localAI[1] = 0f;
						base.projectile.localAI[0] = 0f;
						return;
					}
				}
				else if (base.projectile.localAI[0] == 4f)
				{
					if (Main.rand.Next(5) == 0)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.Center.X + (float)Main.rand.Next(-150, 150), base.projectile.Center.Y + (float)Main.rand.Next(4, 8)), new Vector2(0f, 0f), ModContent.ProjectileType<UkkoRainHealing>(), 0, 0f, base.projectile.owner, 0f, 0f);
					}
					float[] localAI4 = base.projectile.localAI;
					int num5 = 1;
					float num2 = localAI4[num5] + 1f;
					localAI4[num5] = num2;
					if (num2 >= 180f)
					{
						base.projectile.localAI[1] = 0f;
						base.projectile.localAI[0] = 0f;
					}
				}
			}
		}
	}
}
