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
			Projectile projectile = base.projectile;
			int num1068 = projectile.frameCounter + 1;
			projectile.frameCounter = num1068;
			if (num1068 >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num1068 = projectile2.frame + 1;
				projectile2.frame = num1068;
				if (num1068 >= 7)
				{
					base.projectile.frame = 2;
				}
			}
			if (base.projectile.direction == 0)
			{
				base.projectile.direction = Main.player[base.projectile.owner].direction;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f, (float)(255 - base.projectile.alpha) * 0.4f / 255f);
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
				float num1065 = 30f;
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
					base.projectile.ai[0] = -60f;
					base.projectile.ai[1] = num1066 + (float)num1067 * 3.1415927f / 16f;
					base.projectile.netUpdate = true;
					if (base.projectile.owner == Main.myPlayer)
					{
						Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, vector137.X * 15f, vector137.Y * 15f, ModContent.ProjectileType<ConstBlast>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, (float)base.projectile.whoAmI);
					}
				}
			}
		}
	}
}
