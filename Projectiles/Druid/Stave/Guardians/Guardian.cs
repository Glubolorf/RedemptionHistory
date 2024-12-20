using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave.Guardians
{
	public abstract class Guardian : ModProjectile
	{
		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= Main.projFrames[base.projectile.type] - 1)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= Main.projFrames[base.projectile.type])
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			if (player.HasBuff(this.BuffID))
			{
				base.projectile.timeLeft = 2;
			}
			base.projectile.spriteDirection = player.direction;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 1f)
			{
				this.BirthEffects(player);
				Main.PlaySound(SoundID.Item74, base.projectile.position);
			}
			base.projectile.velocity = Vector2.Zero;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.position.X = player.Center.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = player.Top.Y - (float)base.projectile.height - 16f - base.projectile.ai[0];
			this.ActiveUpdate(player);
		}

		public virtual void BirthEffects(Player player)
		{
		}

		public virtual void ActiveUpdate(Player player)
		{
		}

		protected int BuffID = -1;
	}
}
