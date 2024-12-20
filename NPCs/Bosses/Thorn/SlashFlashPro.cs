using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class SlashFlashPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flash");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 46;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 50;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 1f)
			{
				base.projectile.alpha += 10;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
					return;
				}
			}
			else
			{
				base.projectile.alpha -= 10;
				if (base.projectile.alpha <= 0)
				{
					base.projectile.localAI[0] = 1f;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item71, base.projectile.position);
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<SlashPro1>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
		}
	}
}
