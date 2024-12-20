using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class PoisonShard : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Poison Shard");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(94);
			this.aiType = 94;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(20, 160, false);
		}
	}
}
