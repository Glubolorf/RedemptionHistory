using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class SilverwoodArrowPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Silverwood Arrow");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.penetrate = 3;
			base.projectile.timeLeft = 600;
			base.projectile.alpha = 0;
			base.projectile.extraUpdates = 1;
			base.projectile.hide = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			if (base.projectile.ai[0] == 1f)
			{
				int npcIndex = (int)base.projectile.ai[1];
				if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
				{
					if (Main.npc[npcIndex].behindTiles)
					{
						drawCacheProjsBehindNPCsAndTiles.Add(index);
						return;
					}
					drawCacheProjsBehindNPCs.Add(index);
					return;
				}
			}
			drawCacheProjsBehindProjectiles.Add(index);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = (height = 10);
			return true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return new bool?(projHitbox.Intersects(targetHitbox));
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
		}

		public bool isStickingToTarget
		{
			get
			{
				return base.projectile.ai[0] == 1f;
			}
			set
			{
				base.projectile.ai[0] = (value ? 1f : 0f);
			}
		}

		public float targetWhoAmI
		{
			get
			{
				return base.projectile.ai[1];
			}
			set
			{
				base.projectile.ai[1] = value;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			this.isStickingToTarget = true;
			this.targetWhoAmI = (float)target.whoAmI;
			base.projectile.velocity = (target.Center - base.projectile.Center) * 0.75f;
			base.projectile.netUpdate = true;
			target.AddBuff(ModContent.BuffType<SilverwoodArrowDebuff>(), 900, false);
			base.projectile.damage = 0;
			Point[] stickingJavelins = new Point[5];
			int javelinIndex = 0;
			for (int i = 0; i < 1000; i++)
			{
				Projectile currentProjectile = Main.projectile[i];
				if (i != base.projectile.whoAmI && currentProjectile.active && currentProjectile.owner == Main.myPlayer && currentProjectile.type == base.projectile.type && currentProjectile.ai[0] == 1f && currentProjectile.ai[1] == (float)target.whoAmI)
				{
					stickingJavelins[javelinIndex++] = new Point(i, currentProjectile.timeLeft);
					if (javelinIndex >= stickingJavelins.Length)
					{
						break;
					}
				}
			}
			if (javelinIndex >= stickingJavelins.Length)
			{
				int oldJavelinIndex = 0;
				for (int j = 1; j < stickingJavelins.Length; j++)
				{
					if (stickingJavelins[j].Y < stickingJavelins[oldJavelinIndex].Y)
					{
						oldJavelinIndex = j;
					}
				}
				Main.projectile[stickingJavelins[oldJavelinIndex].X].Kill();
			}
		}

		public override void AI()
		{
			if (!this.isStickingToTarget)
			{
				this.targetWhoAmI += 1f;
				if (this.targetWhoAmI >= 45f)
				{
					float velXmult = 0.98f;
					float velYmult = 0.35f;
					this.targetWhoAmI = 45f;
					base.projectile.velocity.X = base.projectile.velocity.X * velXmult;
					base.projectile.velocity.Y = base.projectile.velocity.Y + velYmult;
				}
				base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + MathHelper.ToRadians(90f);
			}
			if (this.isStickingToTarget)
			{
				base.projectile.ignoreWater = true;
				base.projectile.tileCollide = false;
				int aiFactor = 15;
				bool killProj = false;
				base.projectile.localAI[0] += 1f;
				bool hitEffect = base.projectile.localAI[0] % 30f == 0f;
				int projTargetIndex = (int)this.targetWhoAmI;
				if (base.projectile.localAI[0] >= (float)(60 * aiFactor) || projTargetIndex < 0 || projTargetIndex >= 200)
				{
					killProj = true;
				}
				else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage)
				{
					base.projectile.Center = Main.npc[projTargetIndex].Center - base.projectile.velocity * 2f;
					base.projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
					if (hitEffect)
					{
						Main.npc[projTargetIndex].HitEffect(0, 1.0);
					}
				}
				else
				{
					killProj = true;
				}
				if (killProj)
				{
					base.projectile.Kill();
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}

		private const float maxTicks = 45f;
	}
}
