using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class ScanPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scan");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 4;
			base.projectile.height = 4;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			int drone = (int)base.projectile.ai[0];
			if (drone < 0 || drone >= 200 || !Main.npc[drone].active || Main.npc[drone].type != ModContent.NPCType<ScannerDrone>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 Pos = new Vector2((npc2.spriteDirection != 1) ? (npc2.Center.X - 5f) : (npc2.Center.X + 5f), npc2.Center.Y - 3f);
			base.projectile.Center = Pos;
			float obj = base.projectile.localAI[1];
			if (!0f.Equals(obj))
			{
				if (1f.Equals(obj))
				{
					base.projectile.localAI[0] += 0.03f;
					if (base.projectile.localAI[0] >= 0.6f)
					{
						base.projectile.localAI[0] = 0.6f;
						base.projectile.localAI[1] = 0f;
					}
				}
			}
			else
			{
				base.projectile.localAI[0] -= 0.03f;
				if (base.projectile.localAI[0] <= -0.6f)
				{
					base.projectile.localAI[0] = -0.6f;
					base.projectile.localAI[1] = 1f;
				}
			}
			base.projectile.rotation = ((npc2.spriteDirection != 1) ? ((float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f) : ((float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) - 1.57f)) + base.projectile.localAI[0];
		}
	}
}
