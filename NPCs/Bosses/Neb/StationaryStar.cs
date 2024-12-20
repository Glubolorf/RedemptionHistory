using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class StationaryStar : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/StationaryStar";
			}
		}

		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("NPCs/Bosses/Neb/" + base.GetType().Name);
				StationaryStar.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Starplane");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 76;
			base.projectile.height = 56;
			base.projectile.hostile = true;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 160;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.glowMask = StationaryStar.customGlowMask;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 4)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 6)
				{
					base.projectile.frame = 0;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 8; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), 2, 2, 58, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)i / 8f * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}

		public static short customGlowMask;
	}
}
