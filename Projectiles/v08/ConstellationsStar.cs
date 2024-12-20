using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class ConstellationsStar : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Constellations");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 30;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 7)
				{
					base.projectile.frame = 2;
				}
			}
			if (base.projectile.direction == 0)
			{
				base.projectile.direction = Main.player[base.projectile.owner].direction;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f);
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
				float num7 = 30f;
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
					base.projectile.ai[0] = -60f;
					base.projectile.ai[1] = num8 + (float)num9 * 3.1415927f / 16f;
					base.projectile.netUpdate = true;
					if (base.projectile.owner == Main.myPlayer)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, vector.X * 15f, vector.Y * 15f, base.mod.ProjectileType("ConstBlast"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, (float)base.projectile.whoAmI);
					}
				}
			}
		}
	}
}
