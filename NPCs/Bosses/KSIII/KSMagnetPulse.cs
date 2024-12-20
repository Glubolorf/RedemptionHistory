using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KSMagnetPulse : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Surge");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			int DustID2 = Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, 92, 0f, 0f, 0, default(Color), 1f);
			Main.dust[DustID2].noGravity = true;
			NPC projAim = Main.npc[(int)base.projectile.ai[0]];
			if (base.projectile.localAI[0] == 0f)
			{
				this.AdjustMagnitude(ref base.projectile.velocity);
				base.projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 800f;
			bool target = false;
			if (projAim.active)
			{
				Vector2 newMove = projAim.Center - base.projectile.Center;
				float distanceTo = (float)Math.Sqrt((double)(newMove.X * newMove.X + newMove.Y * newMove.Y));
				if (distanceTo < distance)
				{
					move = newMove;
					target = true;
				}
			}
			if (target)
			{
				this.AdjustMagnitude(ref move);
				base.projectile.velocity = (10f * base.projectile.velocity + move) / 11f;
				this.AdjustMagnitude(ref base.projectile.velocity);
			}
			if (base.projectile.Hitbox.Intersects(projAim.Hitbox))
			{
				base.projectile.Kill();
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
			if (magnitude > 12f)
			{
				vector *= 12f / magnitude;
			}
		}
	}
}
