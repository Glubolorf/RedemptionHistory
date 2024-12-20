using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class DruidDamagePlayer : ModPlayer
	{
		public static DruidDamagePlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<DruidDamagePlayer>();
		}

		public override void ResetEffects()
		{
			this.ResetVariables();
		}

		public override void UpdateDead()
		{
			this.ResetVariables();
		}

		private void ResetVariables()
		{
			this.druidDamageFlat = 0f;
			this.druidDamage = 1f;
			this.druidKnockback = 0f;
			this.druidCrit = 0;
			this.dragonLeadBonus = false;
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (proj.ranged && this.dragonLeadBonus)
			{
				for (int p = 0; p < 1000; p++)
				{
					if (Main.projectile[p].active && Main.projectile[p].owner == base.player.whoAmI && Main.projectile[p].GetGlobalProjectile<DruidProjectile>().fromSeedbag && (Main.projectile[p].Center - proj.Center).Length() < 400f && Main.projectile[p].modProjectile is DruidPlant)
					{
						((DruidPlant)Main.projectile[p].modProjectile).Fertilize(damage);
					}
				}
			}
		}

		public float druidDamage = 1f;

		public float druidDamageFlat;

		public float druidKnockback;

		public int druidCrit;

		public bool dragonLeadBonus;

		public bool gloomBonus;
	}
}
