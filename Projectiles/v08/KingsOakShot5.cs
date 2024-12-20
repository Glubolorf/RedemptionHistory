using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class KingsOakShot5 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spore");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(567);
			this.aiType = 567;
			base.projectile.magic = false;
			base.projectile.melee = false;
			base.projectile.ranged = false;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int crit2 = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref crit2);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref crit2);
			if (crit2 >= 100 || Main.rand.Next(1, 101) <= crit2)
			{
				crit = true;
			}
		}
	}
}
