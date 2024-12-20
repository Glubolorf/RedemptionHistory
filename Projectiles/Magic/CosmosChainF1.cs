using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class CosmosChainF1 : ModProjectile
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
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			BaseAI.AIVilethorn(base.projectile, 120, 4, 18);
			this.spineEnd = (base.projectile.ai[1] == 12f);
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(ModContent.BuffType<ChainedDebuff>(), 60, false);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 3;
		}

		public override bool PreDraw(SpriteBatch sb, Color drawColor)
		{
			Color newLightColor = new Color(Math.Max(0, (int)CosmosChainF1.lightColor.R + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)CosmosChainF1.lightColor.G + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)CosmosChainF1.lightColor.B + Math.Min(0, -base.projectile.alpha + 20)));
			BaseDrawing.AddLight(base.projectile.Center, newLightColor, 1f);
			if (CosmosChainF1.mainTex == null)
			{
				CosmosChainF1.mainTex = Main.projectileTexture[base.projectile.type];
				CosmosChainF1.endTex = ModContent.GetTexture("Redemption/Projectiles/Magic/CosmosChainF2");
			}
			BaseDrawing.DrawTexture(sb, this.spineEnd ? CosmosChainF1.endTex : CosmosChainF1.mainTex, 0, base.projectile, null, false, default(Vector2));
			return false;
		}

		public static Color lightColor = new Color(70, 0, 70);

		public static Texture2D mainTex;

		public static Texture2D endTex;

		public bool spineEnd;
	}
}
