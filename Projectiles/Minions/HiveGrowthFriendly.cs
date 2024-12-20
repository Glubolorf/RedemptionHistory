using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class HiveGrowthFriendly : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hive Growth");
			Main.projFrames[base.projectile.type] = 4;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.width = 26;
			base.projectile.height = 26;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 0f;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			this.aiType = 317;
		}

		public override void AI()
		{
			bool flag20 = base.projectile.type == ModContent.ProjectileType<HiveGrowthFriendly>();
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (flag20)
			{
				if (player.dead)
				{
					modPlayer.infectedHeart = false;
				}
				if (modPlayer.infectedHeart)
				{
					base.projectile.timeLeft = 2;
				}
			}
			for (int num526 = 0; num526 < 1000; num526++)
			{
				if (num526 != base.projectile.whoAmI && Main.projectile[num526].active && Main.projectile[num526].owner == base.projectile.owner && Main.projectile[num526].type == base.projectile.type && Math.Abs(base.projectile.position.X - Main.projectile[num526].position.X) + Math.Abs(base.projectile.position.Y - Main.projectile[num526].position.Y) < (float)base.projectile.width)
				{
					if (base.projectile.position.X < Main.projectile[num526].position.X)
					{
						base.projectile.velocity.X = base.projectile.velocity.X - 0.05f;
					}
					else
					{
						base.projectile.velocity.X = base.projectile.velocity.X + 0.05f;
					}
					if (base.projectile.position.Y < Main.projectile[num526].position.Y)
					{
						base.projectile.velocity.Y = base.projectile.velocity.Y - 0.05f;
					}
					else
					{
						base.projectile.velocity.Y = base.projectile.velocity.Y + 0.05f;
					}
				}
			}
			base.projectile.localAI[0] += 1f;
			bool projMax = player.ownedProjectileCounts[ModContent.ProjectileType<HiveGrowthFriendly>()] <= 5;
			if (base.projectile.localAI[0] >= 600f)
			{
				if (projMax && base.projectile.ai[0] != 1f && Main.myPlayer == base.projectile.owner)
				{
					Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<HiveGrowthFriendly>(), 200, 1f, player.whoAmI, 1f, 0f);
				}
				base.projectile.localAI[0] = 0f;
			}
			float num527 = base.projectile.position.X;
			float num528 = base.projectile.position.Y;
			float num529 = 900f;
			bool flag19 = false;
			if (base.projectile.ai[0] == 0f)
			{
				for (int num530 = 0; num530 < 200; num530++)
				{
					if (Main.npc[num530].CanBeChasedBy(base.projectile, false))
					{
						float num531 = Main.npc[num530].position.X + (float)(Main.npc[num530].width / 2);
						float num532 = Main.npc[num530].position.Y + (float)(Main.npc[num530].height / 2);
						float num533 = Math.Abs(base.projectile.position.X + (float)(base.projectile.width / 2) - num531) + Math.Abs(base.projectile.position.Y + (float)(base.projectile.height / 2) - num532);
						if (num533 < num529 && Collision.CanHit(base.projectile.position, base.projectile.width, base.projectile.height, Main.npc[num530].position, Main.npc[num530].width, Main.npc[num530].height))
						{
							num529 = num533;
							num527 = num531;
							num528 = num532;
							flag19 = true;
						}
					}
				}
			}
			else
			{
				base.projectile.tileCollide = false;
			}
			if (!flag19)
			{
				base.projectile.friendly = true;
				float num534 = 8f;
				if (base.projectile.ai[0] == 1f)
				{
					num534 = 12f;
				}
				Vector2 vector38 = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
				float num535 = Main.player[base.projectile.owner].Center.X - vector38.X;
				float num536 = Main.player[base.projectile.owner].Center.Y - vector38.Y - 60f;
				float num537 = (float)Math.Sqrt((double)(num535 * num535 + num536 * num536));
				if (num537 < 100f && base.projectile.ai[0] == 1f && !Collision.SolidCollision(base.projectile.position, base.projectile.width, base.projectile.height))
				{
					base.projectile.ai[0] = 0f;
				}
				if (num537 > 2000f)
				{
					base.projectile.position.X = Main.player[base.projectile.owner].Center.X - (float)(base.projectile.width / 2);
					base.projectile.position.Y = Main.player[base.projectile.owner].Center.Y - (float)(base.projectile.width / 2);
				}
				if (num537 > 70f)
				{
					num537 = num534 / num537;
					num535 *= num537;
					num536 *= num537;
					base.projectile.velocity.X = (base.projectile.velocity.X * 20f + num535) / 21f;
					base.projectile.velocity.Y = (base.projectile.velocity.Y * 20f + num536) / 21f;
				}
				else
				{
					if (base.projectile.velocity.X == 0f && base.projectile.velocity.Y == 0f)
					{
						base.projectile.velocity.X = -0.15f;
						base.projectile.velocity.Y = -0.05f;
					}
					base.projectile.velocity *= 1.01f;
				}
				base.projectile.friendly = false;
				base.projectile.rotation = base.projectile.velocity.X * 0.05f;
				base.projectile.frameCounter++;
				if (base.projectile.frameCounter >= 5)
				{
					base.projectile.frameCounter = 0;
					base.projectile.frame++;
				}
				if (base.projectile.frame >= 4)
				{
					base.projectile.frame = 0;
				}
				if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
				{
					base.projectile.spriteDirection = -base.projectile.direction;
					return;
				}
			}
			else
			{
				if (base.projectile.ai[1] == -1f)
				{
					base.projectile.ai[1] = 17f;
				}
				if (base.projectile.ai[1] > 0f)
				{
					base.projectile.ai[1] -= 1f;
				}
				if (base.projectile.ai[1] == 0f)
				{
					base.projectile.friendly = true;
					float num538 = 8f;
					Vector2 vector39 = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
					float num539 = num527 - vector39.X;
					float num540 = num528 - vector39.Y;
					float num541 = (float)Math.Sqrt((double)(num539 * num539 + num540 * num540));
					if (num541 < 100f)
					{
						num538 = 10f;
					}
					num541 = num538 / num541;
					num539 *= num541;
					num540 *= num541;
					base.projectile.velocity.X = (base.projectile.velocity.X * 14f + num539) / 15f;
					base.projectile.velocity.Y = (base.projectile.velocity.Y * 14f + num540) / 15f;
				}
				else
				{
					base.projectile.friendly = false;
					if (Math.Abs(base.projectile.velocity.X) + Math.Abs(base.projectile.velocity.Y) < 10f)
					{
						base.projectile.velocity *= 1.05f;
					}
				}
				base.projectile.rotation = base.projectile.velocity.X * 0.05f;
				if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
				{
					base.projectile.spriteDirection = -base.projectile.direction;
					return;
				}
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 4;
		}

		public override bool MinionContactDamage()
		{
			return true;
		}
	}
}
