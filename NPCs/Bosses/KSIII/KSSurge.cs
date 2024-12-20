using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KSSurge : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Core Surge");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 100;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 60;
		}

		public override void AI()
		{
			int boss = (int)base.projectile.ai[0];
			if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != ModContent.NPCType<KS3_Body>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.Center = npc2.Center;
			base.projectile.scale += 0.2f;
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.alpha += 10;
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && !player.dead && base.projectile.Distance(player.Center) < 200f)
				{
					player.AddBuff(144, 180, true);
				}
			}
		}
	}
}
