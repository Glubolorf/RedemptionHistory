using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	internal class XeniumSaberSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Slash");
			Main.projFrames[base.projectile.type] = 20;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 108;
			base.projectile.height = 72;
			base.projectile.melee = true;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.ownerHitCheck = true;
			base.projectile.alpha = 80;
			base.projectile.penetrate = -1;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num2 = projectile.frameCounter + 1;
			projectile.frameCounter = num2;
			if (num2 >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num2 = projectile2.frame + 1;
				projectile2.frame = num2;
				if (num2 >= 20)
				{
					base.projectile.frame = 0;
				}
				if (base.projectile.frame % 4 == 0)
				{
					Main.PlaySound(SoundID.Item1, base.projectile.position);
				}
			}
			Player projOwner = Main.player[base.projectile.owner];
			if (!projOwner.channel)
			{
				base.projectile.Kill();
			}
			float num = MathHelper.ToRadians(0f);
			Vector2 vector = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			if (base.projectile.spriteDirection == -1)
			{
				num = MathHelper.ToRadians(180f);
			}
			if (Main.myPlayer == base.projectile.owner)
			{
				float scaleFactor6 = 1f;
				if (projOwner.inventory[projOwner.selectedItem].shoot == base.projectile.type)
				{
					scaleFactor6 = projOwner.inventory[projOwner.selectedItem].shootSpeed * base.projectile.scale;
				}
				Vector2 vector2 = Main.MouseWorld - vector;
				vector2.Normalize();
				if (Utils.HasNaNs(vector2))
				{
					vector2 = Vector2.UnitX * (float)projOwner.direction;
				}
				vector2 *= scaleFactor6;
				if (vector2.X != base.projectile.velocity.X || vector2.Y != base.projectile.velocity.Y)
				{
					base.projectile.netUpdate = true;
				}
				base.projectile.velocity = vector2;
				if (projOwner.noItems || projOwner.CCed || projOwner.dead || !projOwner.active)
				{
					base.projectile.Kill();
				}
				base.projectile.netUpdate = true;
			}
			base.projectile.Center + base.projectile.velocity * 3f;
			base.projectile.position = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true) - base.projectile.Size / 2f;
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + num;
			base.projectile.spriteDirection = base.projectile.direction;
			base.projectile.timeLeft = 2;
			projOwner.ChangeDir(base.projectile.direction);
			projOwner.heldProj = base.projectile.whoAmI;
			projOwner.itemTime = 2;
			projOwner.itemAnimation = 2;
			projOwner.itemRotation = (float)Math.Atan2((double)(base.projectile.velocity.Y * (float)base.projectile.direction), (double)(base.projectile.velocity.X * (float)base.projectile.direction));
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(0, 200, 0, 0) * (1f - (float)base.projectile.alpha / 255f));
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 3;
		}
	}
}
