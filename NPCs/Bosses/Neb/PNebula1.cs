using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class PNebula1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("NPCs/Bosses/Neb/" + base.GetType().Name);
				PNebula1.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Piercing Nebula");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 180;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
			base.projectile.glowMask = PNebula1.customGlowMask;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 30f)
			{
				Main.PlaySound(SoundID.Item125, (int)base.projectile.position.X, (int)base.projectile.position.Y);
			}
			if (base.projectile.localAI[0] >= 30f)
			{
				if (this.proType != 0)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f) - base.projectile.velocity, 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID].velocity *= 0f;
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				if (this.originalVelocity == Vector2.Zero)
				{
					this.originalVelocity = base.projectile.velocity;
				}
				if (this.proType != 0)
				{
					if (this.offsetLeft)
					{
						this.vectorOffset -= 0.5f;
						if (this.vectorOffset <= -1.3f)
						{
							this.vectorOffset = -1.3f;
							this.offsetLeft = false;
						}
					}
					else
					{
						this.vectorOffset += 0.5f;
						if (this.vectorOffset >= 1.3f)
						{
							this.vectorOffset = 1.3f;
							this.offsetLeft = true;
						}
					}
					float velRot = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
					base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(base.projectile.velocity.Length(), 0f), velRot + this.vectorOffset * 0.5f);
				}
				base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
				base.projectile.spriteDirection = 1;
				base.projectile.hostile = true;
				return;
			}
			base.projectile.hostile = false;
		}

		public override bool ShouldUpdatePosition()
		{
			return base.projectile.localAI[0] >= 30f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return base.projectile.localAI[0] >= 30f;
		}

		public int proType;

		public static short customGlowMask;

		public float vectorOffset;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
