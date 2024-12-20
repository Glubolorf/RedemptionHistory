using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	public class MossGoliath_nom : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mossy Goliath");
			Main.projFrames[base.projectile.type] = 4;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 46;
			base.projectile.height = 64;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.hide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 120;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] >= 60f)
			{
				base.projectile.alpha += 10;
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			float[] localAI = base.projectile.localAI;
			int num = 0;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 > 12f)
			{
				base.projectile.velocity.Y = 0f;
				Projectile projectile = base.projectile;
				int num3 = projectile.frameCounter + 1;
				projectile.frameCounter = num3;
				if (num3 >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num3 = projectile2.frame + 1;
					projectile2.frame = num3;
					if (num3 >= 4)
					{
						base.projectile.frame = 3;
					}
				}
			}
			if (base.projectile.localAI[0] == 32f)
			{
				Main.PlaySound(SoundID.Item2, base.projectile.position);
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int width = target.width;
			int height = target.height;
			if (target.velocity.X != 0f && target.velocity.Y != 0f && !target.noGravity)
			{
				target.velocity.Y = -15f * target.knockBackResist;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 5, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.6f;
			}
		}
	}
}
