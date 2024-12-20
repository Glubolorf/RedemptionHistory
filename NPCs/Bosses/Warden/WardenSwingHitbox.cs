using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class WardenSwingHitbox : ModProjectile
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
			base.DisplayName.SetDefault("The Warden");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 174;
			base.projectile.height = 174;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 10;
		}

		public override void AI()
		{
			int boss = (int)base.projectile.ai[0];
			if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != ModContent.NPCType<WardenIdle>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 HitPos = new Vector2((npc2.spriteDirection == -1) ? (npc2.Center.X - 60f) : (npc2.Center.X + 60f), npc2.Center.Y);
			base.projectile.Center = HitPos;
		}
	}
}
