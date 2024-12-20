using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class RingStaveProFright : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/Druid/Stave/RingStaveProFright";
			}
		}

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
				RingStaveProFright.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Ring of Fright");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 180;
			base.projectile.glowMask = RingStaveProFright.customGlowMask;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			base.projectile.velocity.X = 0f;
			base.projectile.velocity.Y = 0f;
			float[] localAI = base.projectile.localAI;
			int num = 1;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 == 1f)
			{
				for (int i = 0; i < 8; i++)
				{
					double angle = (double)i * 0.7853981633974483;
					this.vector.X = (float)(Math.Sin(angle) * 11.0);
					this.vector.Y = (float)(Math.Cos(angle) * 11.0);
					Dust dust = Main.dust[Dust.NewDust(base.projectile.Center + this.vector - new Vector2(4f, 4f), 1, 1, ModContent.DustType<FrightDust>(), 0f, 0f, 100, default(Color), 2f)];
					dust.noGravity = true;
					dust.velocity *= 0f;
				}
			}
			if (base.projectile.localAI[0] == 0f)
			{
				if (Main.rand.Next(10) == 0)
				{
					base.projectile.localAI[0] = 2f;
				}
				else
				{
					base.projectile.localAI[0] = 1f;
				}
			}
			if (base.projectile.localAI[0] == 2f)
			{
				float[] localAI2 = base.projectile.localAI;
				int num3 = 1;
				num2 = localAI2[num3] + 1f;
				localAI2[num3] = num2;
				if (num2 % 30f == 0f)
				{
					int dustType = 57;
					int pieCut = 8;
					for (int j = 0; j < pieCut; j++)
					{
						int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
						Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)j / (float)pieCut * 6.28f);
						Main.dust[dustID].noLight = false;
						Main.dust[dustID].noGravity = true;
					}
				}
				for (int p = 0; p < 255; p++)
				{
					this.clearCheck = Main.player[p];
					if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
					{
						Main.PlaySound(SoundID.Item4.WithVolume(0.3f), base.projectile.position);
						this.clearCheck.AddBuff(ModContent.BuffType<FrightBuff>(), 600, false);
						for (int k = 0; k < 10; k++)
						{
							int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 57, 0f, 0f, 100, default(Color), 2f);
							Main.dust[dustIndex].velocity *= 4f;
						}
						base.projectile.Kill();
					}
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item110.WithVolume(0.3f), base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<FrightDust>(), 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
		}

		public static short customGlowMask;

		private Vector2 vector;

		private Player clearCheck;
	}
}
