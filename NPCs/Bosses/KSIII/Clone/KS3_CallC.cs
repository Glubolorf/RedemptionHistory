using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII.Clone
{
	public class KS3_CallC : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/KSIII/KS3_Call";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Transmission");
			Main.projFrames[base.projectile.type] = 10;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 10)
				{
					base.projectile.frame = 0;
					base.projectile.Kill();
				}
			}
			int slayer = (int)base.projectile.ai[0];
			if (slayer < 0 || slayer >= 200 || !Main.npc[slayer].active || Main.npc[slayer].type != ModContent.NPCType<KS3_Body_Clone>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 CallPos = new Vector2((npc2.spriteDirection == 1) ? (npc2.Center.X + 22f) : (npc2.Center.X - 22f), npc2.Center.Y - 56f);
			base.projectile.Center = CallPos;
			base.projectile.velocity = Vector2.Zero;
			if (npc2.spriteDirection == 1)
			{
				base.projectile.spriteDirection = -1;
				return;
			}
			base.projectile.spriteDirection = 1;
		}
	}
}
