using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class SlayersChakramPro : ModProjectile
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/v08/" + base.GetType().Name + "_Glow");
				SlayersChakramPro.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Cyber Chakram");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 30;
			base.projectile.aiStyle = 3;
			base.projectile.friendly = true;
			base.projectile.melee = true;
			base.projectile.magic = false;
			base.projectile.penetrate = 5;
			base.projectile.timeLeft = 600;
			base.projectile.light = 0.7f;
			base.projectile.extraUpdates = 1;
			base.projectile.glowMask = SlayersChakramPro.customGlowMask;
		}

		public override void AI()
		{
			Player p = Main.player[base.projectile.owner];
			BaseAI.AIBoomerang(base.projectile, ref base.projectile.ai, p.position, p.width, p.height, true, 27f, 35, 1f, 0.6f, false);
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 10f)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<SlayersChakramPro2>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, base.projectile.rotation, 0f);
				base.projectile.localAI[0] = 0f;
			}
		}

		public override bool OnTileCollide(Vector2 velocityChange)
		{
			if (Main.netMode != 2)
			{
				Collision.HitTiles(base.projectile.position, base.projectile.velocity, base.projectile.width, base.projectile.height);
				Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			}
			BaseAI.TileCollideBoomerang(base.projectile, ref velocityChange, true);
			return false;
		}

		public static short customGlowMask;
	}
}
