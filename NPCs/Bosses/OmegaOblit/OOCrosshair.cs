using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	public class OOCrosshair : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crosshair");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 600;
			this.drawOffsetX = -18;
			this.drawOriginOffsetY = -18;
		}

		public override void AI()
		{
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
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f);
			int boss = (int)base.projectile.ai[0];
			if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != ModContent.NPCType<OO>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.localAI[1] += 1f;
			if (base.projectile.localAI[1] == 30f)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MissileFire1").WithVolume(0.8f).WithPitchVariance(0.1f), (int)npc2.position.X, (int)npc2.position.Y);
				}
				Projectile.NewProjectile(new Vector2((npc2.spriteDirection != 1) ? (npc2.Center.X + 26f) : (npc2.Center.X - 26f), npc2.Center.Y - 70f), new Vector2(0f, -15f), ModContent.ProjectileType<OmegaMissile2>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, (float)base.projectile.whoAmI, 0f);
			}
		}
	}
}
