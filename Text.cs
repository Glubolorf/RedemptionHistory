using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class Text : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetDefaults()
		{
			base.projectile.width = 1;
			base.projectile.height = 1;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Texts modPlayer = Main.player[base.projectile.owner].GetModPlayer<Texts>();
			modPlayer.text = true;
			modPlayer.BossID = (int)base.projectile.ai[0];
			modPlayer.TextID = (int)base.projectile.ai[1];
			base.projectile.velocity.X = 0f;
			base.projectile.velocity.Y = 0f;
		}
	}
}
