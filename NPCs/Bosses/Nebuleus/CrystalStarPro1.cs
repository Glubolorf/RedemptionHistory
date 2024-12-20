using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class CrystalStarPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("NPCs/Bosses/Nebuleus/" + base.GetType().Name + "_Glow");
				CrystalStarPro1.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Crystal Star");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 38;
			base.projectile.height = 38;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 120;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
			base.projectile.glowMask = CrystalStarPro1.customGlowMask;
		}

		public override bool PreAI()
		{
			base.projectile.rotation += 0.04f;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item27, (int)base.projectile.position.X, (int)base.projectile.position.Y);
			int pieCut = 5;
			for (int i = 0; i < pieCut; i++)
			{
				int projID = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<CrystalStarPro2>(), 30, 3f, 255, 0f, 0f);
				Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)pieCut * 6.28f);
			}
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

		public static short customGlowMask;
	}
}
