using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb.Phase2
{
	public class CosmicEye2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/CosmicEye";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cosmic Eye");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 52;
			base.projectile.height = 46;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 500;
		}

		public override void AI()
		{
			NPC npc = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + 1.5707964f;
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.localAI[0] == 0f)
			{
				base.projectile.alpha -= 20;
				if (base.projectile.alpha <= 0)
				{
					base.projectile.alpha = 0;
					if (Main.myPlayer == base.projectile.owner)
					{
						Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, ModContent.ProjectileType<CosmicEyeBeam2>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, (float)base.projectile.whoAmI, 0f);
					}
					base.projectile.localAI[0] = 1f;
					return;
				}
			}
			else
			{
				base.projectile.localAI[0] += 1f;
				if (base.projectile.localAI[0] >= 300f)
				{
					base.projectile.alpha += 6;
					if (base.projectile.alpha >= 255)
					{
						base.projectile.Kill();
					}
				}
			}
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}
	}
}
