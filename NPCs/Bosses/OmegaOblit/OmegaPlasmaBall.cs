using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	public class OmegaPlasmaBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Plasma Orb");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 48;
			base.projectile.height = 48;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 100;
			base.projectile.timeLeft = 600;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			if (base.projectile.localAI[0] == 0f)
			{
				int dustType = 235;
				int pieCut = 16;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			base.projectile.velocity *= 0.98f;
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && !proj.minion && proj.friendly && proj.damage > 5)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BallFire").WithVolume(0.9f).WithPitchVariance(0.1f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
					}
					for (int j = 0; j < 8; j++)
					{
						Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 235, 0f, 0f, 100, default(Color), 2f);
						dust.velocity = -base.projectile.DirectionTo(dust.position) * 5f;
					}
					for (int k = 0; k < 4; k++)
					{
						float Speed = Utils.NextFloat(Main.rand, 9f, 19f);
						Vector2 vector8 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y);
						int damage = 36;
						int type = ModContent.ProjectileType<OmegaBlast>();
						float rotation = (float)Math.Atan2((double)(vector8.Y - player.Center.Y), (double)(vector8.X - player.Center.X));
						Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + Utils.NextFloat(Main.rand, -3f, 3f), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + Utils.NextFloat(Main.rand, -3f, 3f), type, damage, 0f, 0, 0f, 0f);
					}
					base.projectile.Kill();
					proj.Kill();
				}
			}
			Vector2 move = Vector2.Zero;
			float distance = 50f;
			bool target = false;
			for (int l = 0; l < 200; l++)
			{
				if (Main.player[l].active)
				{
					Vector2 newMove = Main.player[l].Center - base.projectile.Center;
					float distanceTo = (float)Math.Sqrt((double)(newMove.X * newMove.X + newMove.Y * newMove.Y));
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				this.AdjustMagnitude(ref move);
				base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 8; i++)
			{
				Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 235, 0f, 0f, 100, default(Color), 2f);
				dust.velocity = -base.projectile.DirectionTo(dust.position) * 5f;
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 6f)
			{
				vector *= 8f / magnitude;
			}
		}
	}
}
