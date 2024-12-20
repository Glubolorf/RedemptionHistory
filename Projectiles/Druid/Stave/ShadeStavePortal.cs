using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Cooldowns;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class ShadeStavePortal : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Portal");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 22;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			this.drawOffsetX = -8;
			this.drawOriginOffsetY = -16;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			bool boss = false;
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.boss)
				{
					boss = true;
					break;
				}
			}
			if (!boss)
			{
				for (int p = 0; p < 255; p++)
				{
					this.clearCheck = Main.player[p];
					if (this.clearCheck.active && !this.clearCheck.dead && p == base.projectile.owner && !this.clearCheck.HasBuff(ModContent.BuffType<ShadePortalCooldown>()) && Main.player[p].Hitbox.Intersects(base.projectile.Hitbox))
					{
						for (int g = 0; g < 1000; g++)
						{
							this.clearCheck2 = Main.projectile[g];
							if (this.clearCheck2.active && this.clearCheck2.identity != base.projectile.identity && this.clearCheck2.type == ModContent.ProjectileType<ShadeStavePortal>())
							{
								Main.PlaySound(SoundID.NPCDeath52, player.position);
								this.clearCheck.Center = this.clearCheck2.Center;
								for (int j = 0; j < Main.rand.Next(8, 16); j++)
								{
									Projectile.NewProjectile(this.clearCheck2.Center.X, this.clearCheck2.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PortalShadesoul>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
								}
								int dustType = 261;
								int pieCut = 16;
								for (int k = 0; k < pieCut; k++)
								{
									int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
									Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)k / (float)pieCut * 6.28f);
									Main.dust[dustID].noLight = false;
									Main.dust[dustID].noGravity = true;
									int dustID2 = Dust.NewDust(new Vector2(this.clearCheck2.Center.X - 1f, this.clearCheck2.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
									Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)k / (float)pieCut * 6.28f);
									Main.dust[dustID2].noLight = false;
									Main.dust[dustID2].noGravity = true;
								}
								this.clearCheck.AddBuff(ModContent.BuffType<ShadePortalCooldown>(), 300, true);
								break;
							}
						}
					}
				}
			}
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, true, player.MinionAttackTargetNPC, null) && base.projectile.localAI[0] % 90f == 0f)
			{
				Main.PlaySound(SoundID.NPCDeath52.WithVolume(0.5f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
				for (int l = 0; l < Main.rand.Next(2, 7); l++)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(18f, Utils.ToRotation(this.target.Center - base.projectile.Center) + Utils.NextFloat(Main.rand, -20f, 20f)), ModContent.ProjectileType<PortalShadesoul>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
				}
			}
			if (player.dead)
			{
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].velocity *= 1f;
			}
		}

		private Player clearCheck;

		private Projectile clearCheck2;

		private NPC target;
	}
}
