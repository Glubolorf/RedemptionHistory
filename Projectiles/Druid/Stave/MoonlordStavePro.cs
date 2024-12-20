using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class MoonlordStavePro : ModProjectile
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/Druid/Stave/" + base.GetType().Name);
				MoonlordStavePro.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Phantasmal Mine");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 44;
			base.projectile.height = 44;
			base.projectile.aiStyle = 0;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 600;
			base.projectile.penetrate = 1;
			base.projectile.alpha = 0;
			base.projectile.friendly = true;
			base.projectile.glowMask = MoonlordStavePro.customGlowMask;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			Vector2 vector = new Vector2(base.projectile.ai[0], base.projectile.ai[1]) - base.projectile.Center;
			if ((double)vector.Length() < (double)base.projectile.velocity.Length())
			{
				base.projectile.velocity *= 0f;
			}
			else
			{
				vector.Normalize();
				base.projectile.velocity = Vector2.Lerp(base.projectile.velocity, vector * 11.2f, 0.1f);
			}
			base.projectile.netUpdate = true;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 1f)
			{
				int dustType = 229;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<MoonlordStaveBoom>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			if (this.auraDirection)
			{
				this.auraPercent += 0.1f;
				this.auraDirection = (this.auraPercent < 1f);
			}
			else
			{
				this.auraPercent -= 0.1f;
				this.auraDirection = (this.auraPercent <= 0f);
			}
			Rectangle frame = BaseDrawing.GetFrame(0, 30, 30, 0, 0);
			BaseDrawing.DrawAura(sb, Main.projectileTexture[base.projectile.type], 0, base.projectile.position, base.projectile.width, base.projectile.height, this.auraPercent, 1.2f, base.projectile.scale, base.projectile.rotation, -1, 1, frame, 0f, 0f, new Color?(Color.White));
			BaseDrawing.DrawTexture(sb, Main.projectileTexture[base.projectile.type], 0, base.projectile.position, base.projectile.width, base.projectile.height, base.projectile.scale, base.projectile.rotation, -1, 1, frame, new Color?(base.projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE)), false, default(Vector2));
			return false;
		}

		public static short customGlowMask;

		public float auraPercent;

		public bool auraDirection = true;
	}
}
