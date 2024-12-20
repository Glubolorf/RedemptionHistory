using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Pets
{
	public class XenomiteElementalPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pet Xenomite Elemental");
			Main.projFrames[base.projectile.type] = 8;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(703);
			base.projectile.width = 34;
			base.projectile.height = 66;
			this.aiType = 703;
		}

		public override bool PreAI()
		{
			Main.player[base.projectile.owner].petFlagDD2Gato = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.xenoPet = false;
			}
			if (modPlayer.xenoPet)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
