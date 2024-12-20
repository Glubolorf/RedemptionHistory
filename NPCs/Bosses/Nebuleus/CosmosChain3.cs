using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class CosmosChain3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chain of the Cosmos");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
			base.projectile.aiStyle = -1;
			base.projectile.timeLeft = 30;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(base.mod.BuffType("ChainedDebuff"), 60, true);
		}
	}
}
