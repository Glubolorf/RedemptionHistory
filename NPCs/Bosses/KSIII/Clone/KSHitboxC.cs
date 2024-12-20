using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII.Clone
{
	public class KSHitboxC : ModProjectile
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
			base.DisplayName.SetDefault("King Slayer III");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 88;
			base.projectile.height = 84;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 30;
		}

		public override void AI()
		{
			int slayer = (int)base.projectile.ai[0];
			if (slayer < 0 || slayer >= 200 || !Main.npc[slayer].active || Main.npc[slayer].type != ModContent.NPCType<KS3_Body_Clone>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 HitPos = new Vector2((npc2.spriteDirection == -1) ? (npc2.Center.X - 8f) : (npc2.Center.X + 8f), npc2.Center.Y - 5f);
			base.projectile.Center = HitPos;
			base.projectile.velocity = Vector2.Zero;
			if (npc2.ai[1] != 1f || npc2.ai[0] != 5f)
			{
				base.projectile.Kill();
			}
		}
	}
}
