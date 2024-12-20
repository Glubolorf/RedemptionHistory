using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Pets
{
	public class ChickenPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pet Chicken");
			Main.projFrames[base.projectile.type] = 8;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(208);
			this.aiType = 208;
		}

		public override bool PreAI()
		{
			Player player = Main.player[base.projectile.owner];
			player.parrot = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.dead)
			{
				modPlayer.examplePet = false;
			}
			if (modPlayer.examplePet)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
