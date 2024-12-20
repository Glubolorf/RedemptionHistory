using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class CosmosChain1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chain of the Cosmos");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
			base.projectile.aiStyle = -1;
			base.projectile.timeLeft = 320;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			BaseAI.AIVilethorn(base.projectile, 120, 4, 50);
			this.spineEnd = (base.projectile.ai[1] == 50f);
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 5f)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("CosmosChain3"), base.projectile.damage, base.projectile.knockBack, 255, 0f, 0f);
			}
		}

		public override bool PreDraw(SpriteBatch sb, Color drawColor)
		{
			Color newLightColor = new Color(Math.Max(0, (int)CosmosChain1.lightColor.R + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)CosmosChain1.lightColor.G + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)CosmosChain1.lightColor.B + Math.Min(0, -base.projectile.alpha + 20)));
			BaseDrawing.AddLight(base.projectile.Center, newLightColor, 1f);
			if (CosmosChain1.mainTex == null)
			{
				CosmosChain1.mainTex = Main.projectileTexture[base.projectile.type];
				CosmosChain1.endTex = ModContent.GetTexture("Redemption/NPCs/Bosses/Nebuleus/CosmosChain2");
			}
			BaseDrawing.DrawTexture(sb, this.spineEnd ? CosmosChain1.endTex : CosmosChain1.mainTex, 0, base.projectile, null, false, default(Vector2));
			return false;
		}

		public static Color lightColor = new Color(70, 0, 70);

		public static Texture2D mainTex;

		public static Texture2D endTex;

		public bool spineEnd;
	}
}
