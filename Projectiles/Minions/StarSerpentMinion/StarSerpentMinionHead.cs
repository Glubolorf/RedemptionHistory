using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions.StarSerpentMinion
{
	public class StarSerpentMinionHead : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Baby Star Serpent");
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 28;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.netImportant = true;
			base.projectile.tileCollide = false;
			base.projectile.minion = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.timeLeft *= 5;
			base.projectile.usesLocalNPCImmunity = true;
			base.projectile.localNPCHitCooldown = 5;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 5;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(Color.White);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D13 = Main.projectileTexture[base.projectile.type];
			int num214 = Main.projectileTexture[base.projectile.type].Height / Main.projFrames[base.projectile.type];
			int y6 = num214 * base.projectile.frame;
			Main.spriteBatch.Draw(texture2D13, base.projectile.Center - Main.screenPosition + new Vector2(0f, base.projectile.gfxOffY), new Rectangle?(new Rectangle(0, y6, texture2D13.Width, num214)), base.projectile.GetAlpha(Color.White), base.projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), base.projectile.scale, (base.projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if ((int)Main.time % 120 == 0)
			{
				base.projectile.netUpdate = true;
			}
			if (!player.active)
			{
				base.projectile.active = false;
				return;
			}
			if (player.dead)
			{
				modPlayer.StarSerpentMinion = false;
			}
			if (modPlayer.StarSerpentMinion)
			{
				base.projectile.timeLeft = 2;
			}
			int num1038 = 30;
			Vector2 center = player.Center;
			float num1039 = 700f;
			float num1040 = 1000f;
			int num1041 = -1;
			if (base.projectile.Distance(center) > 2000f)
			{
				base.projectile.Center = center;
				base.projectile.netUpdate = true;
			}
			if (true)
			{
				NPC ownerMinionAttackTargetNPC5 = base.projectile.OwnerMinionAttackTargetNPC;
				if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(base.projectile, false) && base.projectile.Distance(ownerMinionAttackTargetNPC5.Center) < num1039 * 2f)
				{
					num1041 = ownerMinionAttackTargetNPC5.whoAmI;
					if (ownerMinionAttackTargetNPC5.boss)
					{
						int whoAmI = ownerMinionAttackTargetNPC5.whoAmI;
					}
					else
					{
						int whoAmI2 = ownerMinionAttackTargetNPC5.whoAmI;
					}
				}
				if (num1041 < 0)
				{
					for (int num1042 = 0; num1042 < 200; num1042++)
					{
						NPC nPC13 = Main.npc[num1042];
						if (nPC13.CanBeChasedBy(base.projectile, false) && player.Distance(nPC13.Center) < num1040 && base.projectile.Distance(nPC13.Center) < num1039)
						{
							num1041 = num1042;
							bool boss = nPC13.boss;
						}
					}
				}
			}
			if (num1041 != -1)
			{
				NPC nPC14 = Main.npc[num1041];
				Vector2 vector132 = nPC14.Center - base.projectile.Center;
				Utils.ToDirectionInt(vector132.X > 0f);
				Utils.ToDirectionInt(vector132.Y > 0f);
				float scaleFactor15 = 0.4f;
				if (vector132.Length() < 600f)
				{
					scaleFactor15 = 0.6f;
				}
				if (vector132.Length() < 300f)
				{
					scaleFactor15 = 0.8f;
				}
				if (vector132.Length() > nPC14.Size.Length() * 0.75f)
				{
					base.projectile.velocity += Vector2.Normalize(vector132) * scaleFactor15 * 1.5f;
					if (Vector2.Dot(base.projectile.velocity, vector132) < 0.25f)
					{
						base.projectile.velocity *= 0.8f;
					}
				}
				float num1043 = 30f;
				if (base.projectile.velocity.Length() > num1043)
				{
					base.projectile.velocity = Vector2.Normalize(base.projectile.velocity) * num1043;
				}
			}
			else
			{
				float num1044 = 0.2f;
				Vector2 vector133 = center - base.projectile.Center;
				if (vector133.Length() < 200f)
				{
					num1044 = 0.12f;
				}
				if (vector133.Length() < 140f)
				{
					num1044 = 0.06f;
				}
				if (vector133.Length() > 100f)
				{
					if (Math.Abs(center.X - base.projectile.Center.X) > 20f)
					{
						base.projectile.velocity.X = base.projectile.velocity.X + num1044 * (float)Math.Sign(center.X - base.projectile.Center.X);
					}
					if (Math.Abs(center.Y - base.projectile.Center.Y) > 10f)
					{
						base.projectile.velocity.Y = base.projectile.velocity.Y + num1044 * (float)Math.Sign(center.Y - base.projectile.Center.Y);
					}
				}
				else if (base.projectile.velocity.Length() > 2f)
				{
					base.projectile.velocity *= 0.96f;
				}
				if (Math.Abs(base.projectile.velocity.Y) < 1f)
				{
					base.projectile.velocity.Y = base.projectile.velocity.Y - 0.1f;
				}
				float num1045 = 15f;
				if (base.projectile.velocity.Length() > num1045)
				{
					base.projectile.velocity = Vector2.Normalize(base.projectile.velocity) * num1045;
				}
			}
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + 1.5707964f;
			int direction = base.projectile.direction;
			base.projectile.direction = (base.projectile.spriteDirection = ((base.projectile.velocity.X > 0f) ? 1 : -1));
			if (direction != base.projectile.direction)
			{
				base.projectile.netUpdate = true;
			}
			float num1046 = MathHelper.Clamp(base.projectile.localAI[0], 0f, 50f);
			base.projectile.position = base.projectile.Center;
			base.projectile.scale = 1f + num1046 * 0.01f;
			base.projectile.width = (base.projectile.height = (int)((float)num1038 * base.projectile.scale));
			base.projectile.Center = base.projectile.position;
			if (base.projectile.alpha > 0)
			{
				base.projectile.alpha -= 42;
				if (base.projectile.alpha < 0)
				{
					base.projectile.alpha = 0;
				}
			}
		}
	}
}
