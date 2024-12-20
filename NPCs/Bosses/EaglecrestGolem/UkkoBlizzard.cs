using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class UkkoBlizzard : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(337);
			base.projectile.hostile = true;
			base.projectile.friendly = false;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ice Spike");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.5f / 255f, (float)(255 - base.projectile.alpha) * 0.7f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(46, 120, true);
			}
			if (Main.rand.Next(4) == 0)
			{
				target.AddBuff(47, 40, true);
			}
		}
	}
}
