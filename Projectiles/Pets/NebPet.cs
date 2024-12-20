using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Pets
{
	public class NebPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebby");
			Main.projFrames[base.projectile.type] = 13;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(236);
			base.projectile.width = 34;
			base.projectile.height = 52;
			this.aiType = 236;
		}

		public override bool PreAI()
		{
			Main.player[base.projectile.owner].dino = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.nebPet = false;
			}
			if (modPlayer.nebPet)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
