using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Hostile
{
	public class SnowyBall2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/Magic/SnowyBall";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chilling Sphere");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(128);
			this.aiType = 128;
			base.projectile.tileCollide = false;
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.timeLeft = 200;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(4) == 0 || (Main.expertMode && Main.rand.Next(2) == 0))
			{
				target.AddBuff(44, 60, true);
			}
			if (Main.rand.Next(6) == 0 || (Main.expertMode && Main.rand.Next(4) == 0))
			{
				target.AddBuff(47, 60, true);
			}
		}
	}
}
