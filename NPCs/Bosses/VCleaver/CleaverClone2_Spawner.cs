using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.VCleaver
{
	public class CleaverClone2_Spawner : ModProjectile
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
			base.DisplayName.SetDefault("Phantom Cleaver");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 98;
			base.projectile.height = 280;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 30;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.localAI[0] % 3f == 0f && Main.myPlayer == base.projectile.owner)
			{
				base.projectile.localAI[1] += 5f;
				Projectile.NewProjectile(base.projectile.Center, new Vector2(base.projectile.localAI[1], 0f), ModContent.ProjectileType<CleaverClone2>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(base.projectile.Center, new Vector2(-base.projectile.localAI[1], 0f), ModContent.ProjectileType<CleaverClone2>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}
