using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Pets
{
	public class TiedemiesPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tiedemies");
			Main.projFrames[base.projectile.type] = 10;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(211);
			this.aiType = 211;
		}

		public override bool PreAI()
		{
			Main.player[base.projectile.owner].wisp = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
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
