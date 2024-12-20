using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class IceSoulPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chilling Soul");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(359);
			this.aiType = 359;
			base.projectile.width = 16;
			base.projectile.height = 16;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(1) == 0))
			{
				target.AddBuff(44, 80, false);
			}
		}
	}
}
