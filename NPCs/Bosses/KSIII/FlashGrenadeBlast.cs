using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class FlashGrenadeBlast : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Explosion");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 60;
			base.projectile.height = 60;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 6;
		}

		public override void AI()
		{
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && !player.dead && base.projectile.Distance(player.Center) < 500f)
				{
					player.AddBuff(31, 180, true);
				}
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(ModContent.BuffType<StunnedDebuff>(), 60, true);
		}
	}
}
