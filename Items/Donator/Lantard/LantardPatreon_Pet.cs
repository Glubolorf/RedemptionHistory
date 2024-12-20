using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Donator.Lantard
{
	public class LantardPatreon_Pet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ralsei");
			Main.projFrames[base.projectile.type] = 10;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(236);
			base.projectile.width = 30;
			base.projectile.height = 48;
			this.aiType = 236;
		}

		public override bool PreAI()
		{
			Main.player[base.projectile.owner].dino = false;
			return true;
		}

		public override void AI()
		{
			if (base.projectile.velocity.Y >= 1f || base.projectile.velocity.Y <= -1f)
			{
				base.projectile.frame = 9;
			}
			else if (base.projectile.velocity.X == 0f)
			{
				base.projectile.frame = 0;
			}
			else
			{
				base.projectile.frameCounter += (int)(base.projectile.velocity.X * 0.5f);
				if (base.projectile.frameCounter >= 5 || base.projectile.frameCounter <= -5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile = base.projectile;
					int num = projectile.frame + 1;
					projectile.frame = num;
					if (num >= 9)
					{
						base.projectile.frame = 1;
					}
				}
			}
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.lantardPet = false;
			}
			if (modPlayer.lantardPet)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
