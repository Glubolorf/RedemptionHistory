using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII.Clone
{
	public class KSWaveC : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/KSIII/KSWave";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 192;
			base.projectile.height = 88;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 100;
			base.projectile.timeLeft = 30;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[base.projectile.type].Width * 0.5f, (float)base.projectile.height * 0.5f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, base.projectile.gfxOffY);
				Color color = base.projectile.GetAlpha(lightColor) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[base.projectile.type], drawPos, null, color, base.projectile.rotation, drawOrigin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			int slayer = (int)base.projectile.ai[0];
			if (slayer < 0 || slayer >= 200 || !Main.npc[slayer].active || Main.npc[slayer].type != ModContent.NPCType<KS3_Body_Clone>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 HitPos = new Vector2(npc2.Center.X, npc2.Center.Y + 40f);
			base.projectile.Center = HitPos;
			base.projectile.velocity = Vector2.Zero;
			if (npc2.ai[1] != 1f || npc2.ai[0] != 5f)
			{
				base.projectile.Kill();
			}
		}
	}
}
