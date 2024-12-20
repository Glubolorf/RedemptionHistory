using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class AncientTimeAura : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Time Aura");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 126;
			base.projectile.height = 126;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 254;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += 0.06f;
			if (base.projectile.localAI[0] <= 60f)
			{
				base.projectile.alpha -= 4;
			}
			if (base.projectile.localAI[0] >= 180f)
			{
				base.projectile.alpha += 4;
			}
			if (base.projectile.alpha >= 255)
			{
				base.projectile.Kill();
			}
			if (base.projectile.localAI[0] > 60f && base.projectile.localAI[0] < 180f)
			{
				if (base.projectile.direction == 0)
				{
					base.projectile.direction = Main.player[base.projectile.owner].direction;
				}
				float num1058 = 1000f;
				if (base.projectile.ai[0] < 0f)
				{
					base.projectile.ai[0] += 1f;
					base.projectile.ai[1] -= (float)base.projectile.direction * 0.3926991f / 50f;
				}
				if (base.projectile.ai[0] == 0f)
				{
					int num1059 = -1;
					float num1060 = num1058;
					NPC ownerMinionAttackTargetNPC6 = base.projectile.OwnerMinionAttackTargetNPC;
					if (ownerMinionAttackTargetNPC6 != null && ownerMinionAttackTargetNPC6.CanBeChasedBy(this, false))
					{
						float num1061 = base.projectile.Distance(ownerMinionAttackTargetNPC6.Center);
						if (num1061 < num1060 && Collision.CanHitLine(base.projectile.Center, 0, 0, ownerMinionAttackTargetNPC6.Center, 0, 0))
						{
							num1060 = num1061;
							num1059 = ownerMinionAttackTargetNPC6.whoAmI;
						}
					}
					if (num1059 < 0)
					{
						for (int num1062 = 0; num1062 < 200; num1062++)
						{
							NPC nPC15 = Main.npc[num1062];
							if (nPC15.CanBeChasedBy(this, false))
							{
								float num1063 = base.projectile.Distance(nPC15.Center);
								if (num1063 < num1060 && Collision.CanHitLine(base.projectile.Center, 0, 0, nPC15.Center, 0, 0))
								{
									num1060 = num1063;
									num1059 = num1062;
								}
							}
						}
					}
					if (num1059 != -1)
					{
						base.projectile.ai[0] = 1f;
						base.projectile.ai[1] = (float)num1059;
						base.projectile.netUpdate = true;
						return;
					}
				}
				if (base.projectile.ai[0] > 0f)
				{
					int num1064 = (int)base.projectile.ai[1];
					if (!Main.npc[num1064].CanBeChasedBy(this, false))
					{
						base.projectile.ai[0] = 0f;
						base.projectile.ai[1] = 0f;
						base.projectile.netUpdate = true;
						return;
					}
					base.projectile.ai[0] += 1f;
					float num1065 = 10f;
					if (base.projectile.ai[0] >= num1065)
					{
						Vector2 vector137 = base.projectile.DirectionTo(Main.npc[num1064].Center);
						if (Utils.HasNaNs(vector137))
						{
							vector137 = Vector2.UnitY;
						}
						float num1066 = Utils.ToRotation(vector137);
						int num1067 = (vector137.X > 0f) ? 1 : -1;
						base.projectile.direction = num1067;
						base.projectile.ai[0] = 0f;
						base.projectile.ai[1] = num1066 + (float)num1067 * 3.1415927f / 16f;
						base.projectile.netUpdate = true;
						if (base.projectile.owner == Main.myPlayer)
						{
							Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, vector137.X * 15f, vector137.Y * 15f, base.mod.ProjectileType("LastRPro5"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, (float)base.projectile.whoAmI);
						}
					}
				}
			}
		}
	}
}
