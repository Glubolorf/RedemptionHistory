﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class GradivinaPro3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gradivina");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 36;
			base.projectile.height = 36;
			base.projectile.aiStyle = 19;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.scale = 1.3f;
			base.projectile.hide = true;
			base.projectile.ownerHitCheck = true;
			base.projectile.melee = true;
			base.projectile.alpha = 0;
		}

		public float MovementFactor
		{
			get
			{
				return base.projectile.ai[0];
			}
			set
			{
				base.projectile.ai[0] = value;
			}
		}

		public override void AI()
		{
			Player projOwner = Main.player[base.projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			base.projectile.direction = projOwner.direction;
			projOwner.heldProj = base.projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			base.projectile.position.X = ownerMountedCenter.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = ownerMountedCenter.Y - (float)(base.projectile.height / 2);
			if (!projOwner.frozen)
			{
				if (this.MovementFactor == 0f)
				{
					this.MovementFactor = 1f;
					base.projectile.netUpdate = true;
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
				{
					this.MovementFactor -= 2.7f;
				}
				else
				{
					this.MovementFactor += 2.5f;
				}
			}
			base.projectile.position += base.projectile.velocity * this.MovementFactor;
			if (projOwner.itemAnimation == 0)
			{
				base.projectile.Kill();
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + MathHelper.ToRadians(135f);
			if (base.projectile.spriteDirection == -1)
			{
				base.projectile.rotation -= MathHelper.ToRadians(90f);
			}
		}
	}
}
