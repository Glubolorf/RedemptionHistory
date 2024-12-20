using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class Plant33 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Thorn Brambles");
			Main.projFrames[base.projectile.type] = 9;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 76;
			base.projectile.height = 68;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.hide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 10)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 9)
				{
					base.projectile.frame = 7;
				}
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.X = projectile.velocity.X * 0f;
			Projectile projectile2 = base.projectile;
			projectile2.velocity.Y = projectile2.velocity.Y + 1f;
			if (base.projectile.localAI[0] >= 160f)
			{
				base.projectile.alpha += 10;
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			if (base.projectile.localAI[0] > 30f && base.projectile.localAI[0] < 160f)
			{
				if (base.projectile.direction == 0)
				{
					base.projectile.direction = Main.player[base.projectile.owner].direction;
				}
				float num = 1000f;
				if (base.projectile.ai[0] < 0f)
				{
					base.projectile.ai[0] += 1f;
					base.projectile.ai[1] -= (float)base.projectile.direction * 0.3926991f / 50f;
				}
				if (base.projectile.ai[0] == 0f)
				{
					int num2 = -1;
					float num3 = num;
					NPC ownerMinionAttackTargetNPC = base.projectile.OwnerMinionAttackTargetNPC;
					if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
					{
						float num4 = base.projectile.Distance(ownerMinionAttackTargetNPC.Center);
						if (num4 < num3 && Collision.CanHitLine(base.projectile.Center, 0, 0, ownerMinionAttackTargetNPC.Center, 0, 0))
						{
							num3 = num4;
							num2 = ownerMinionAttackTargetNPC.whoAmI;
						}
					}
					if (num2 < 0)
					{
						for (int i = 0; i < 200; i++)
						{
							NPC npc = Main.npc[i];
							if (npc.CanBeChasedBy(this, false))
							{
								float num5 = base.projectile.Distance(npc.Center);
								if (num5 < num3 && Collision.CanHitLine(base.projectile.Center, 0, 0, npc.Center, 0, 0))
								{
									num3 = num5;
									num2 = i;
								}
							}
						}
					}
					if (num2 != -1)
					{
						base.projectile.ai[0] = 1f;
						base.projectile.ai[1] = (float)num2;
						base.projectile.netUpdate = true;
						return;
					}
				}
				if (base.projectile.ai[0] > 0f)
				{
					int num6 = (int)base.projectile.ai[1];
					if (!Main.npc[num6].CanBeChasedBy(this, false))
					{
						base.projectile.ai[0] = 0f;
						base.projectile.ai[1] = 0f;
						base.projectile.netUpdate = true;
						return;
					}
					base.projectile.ai[0] += 1f;
					float num7 = 64f;
					if (base.projectile.ai[0] >= num7)
					{
						Vector2 vector = base.projectile.DirectionTo(Main.npc[num6].Center);
						if (Utils.HasNaNs(vector))
						{
							vector = Vector2.UnitY;
						}
						float num8 = Utils.ToRotation(vector);
						int num9 = (vector.X > 0f) ? 1 : -1;
						base.projectile.direction = num9;
						base.projectile.ai[0] = 0f;
						base.projectile.ai[1] = num8 + (float)num9 * 3.1415927f / 16f;
						base.projectile.netUpdate = true;
						if (base.projectile.owner == Main.myPlayer)
						{
							int num10 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, vector.X * 10f, vector.Y * 10f, 55, base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, (float)base.projectile.whoAmI);
							Main.projectile[num10].friendly = true;
							Main.projectile[num10].hostile = false;
							Main.projectile[num10].timeLeft = 160;
						}
					}
				}
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 0f)
			{
				base.projectile.velocity.X = oldVelocity.X * --0f;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 0f)
			{
				base.projectile.velocity.Y = oldVelocity.Y * --0f;
			}
			return false;
		}
	}
}
