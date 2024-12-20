using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class VesselStaffPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadesoul");
			Main.projFrames[base.projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.penetrate = 1;
			base.projectile.magic = true;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 600;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			Player projOwner = Main.player[base.projectile.owner];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 8)
				{
					base.projectile.frame = 0;
				}
			}
			Vector2 Pos = new Vector2((float)Main.rand.Next(-120, 120), (float)Main.rand.Next(-120, 120));
			base.projectile.rotation = (projOwner.channel ? 0f : ((float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f));
			if (base.projectile.ai[1] == 0f)
			{
				this.MoveVector2 = Pos;
				base.projectile.ai[1] = 1f;
			}
			if (projOwner.channel && base.projectile.ai[0] == 0f)
			{
				this.Move(this.MoveVector2, 10f, 30f);
				return;
			}
			if (base.projectile.ai[0] == 0f)
			{
				Main.PlaySound(SoundID.NPCDeath52.WithVolume(0.5f), base.projectile.position);
				base.projectile.timeLeft = 60;
				base.projectile.velocity = base.projectile.DirectionTo(Main.MouseWorld) * 20f;
				base.projectile.ai[0] = 1f;
			}
		}

		private void Move(Vector2 offset, float speed, float turnResistance = 10f)
		{
			Vector2 move = Main.player[base.projectile.owner].Center + offset - base.projectile.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			move = (base.projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > speed)
			{
				move *= speed / magnitude;
			}
			base.projectile.velocity = move;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
			{
				Color.GhostWhite,
				Color.Black,
				Color.GhostWhite
			}));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1f;
			}
		}

		public Vector2 MoveVector2;
	}
}
