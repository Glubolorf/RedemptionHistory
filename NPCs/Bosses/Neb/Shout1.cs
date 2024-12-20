using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class Shout1 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/Shout1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shout!");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 258;
			base.projectile.height = 46;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 60;
			this.drawOriginOffsetY = -100;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.Center = npc2.Center;
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
	}
}
