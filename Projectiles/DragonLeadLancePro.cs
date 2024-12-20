using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class DragonLeadLancePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Projectiles/" + base.GetType().Name + "_Glow");
				DragonLeadLancePro.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.DisplayName.SetDefault("Dragon Slayer's Greatlance");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 28;
			base.projectile.aiStyle = 19;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.scale = 1.3f;
			base.projectile.hide = true;
			base.projectile.ownerHitCheck = true;
			base.projectile.melee = true;
			base.projectile.alpha = 0;
			base.projectile.glowMask = DragonLeadLancePro.customGlowMask;
		}

		public float movementFactor
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
			if (Main.rand.Next(2) == 0)
			{
				int num = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num].noGravity = true;
			}
			Player player = Main.player[base.projectile.owner];
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			base.projectile.direction = player.direction;
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = player.itemAnimation;
			base.projectile.position.X = vector.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = vector.Y - (float)(base.projectile.height / 2);
			if (!player.frozen)
			{
				if (this.movementFactor == 0f)
				{
					this.movementFactor = 1f;
					base.projectile.netUpdate = true;
				}
				if (player.itemAnimation < player.itemAnimationMax / 3)
				{
					this.movementFactor -= 2.4f;
				}
				else
				{
					this.movementFactor += 2.1f;
				}
			}
			base.projectile.position += base.projectile.velocity * this.movementFactor;
			if (player.itemAnimation == 0)
			{
				base.projectile.Kill();
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + MathHelper.ToRadians(135f);
			if (base.projectile.spriteDirection == -1)
			{
				base.projectile.rotation -= MathHelper.ToRadians(90f);
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(24, 160, false);
		}

		public static short customGlowMask;
	}
}
