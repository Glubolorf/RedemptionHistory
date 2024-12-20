using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class EaglecrestMinion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Minion");
			Main.projFrames[base.projectile.type] = 8;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 38;
			base.projectile.height = 42;
			base.projectile.minion = true;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = true;
			base.projectile.netImportant = true;
			base.projectile.alpha = 0;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.minionSlots = 1f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.penetrate == 0)
			{
				base.projectile.Kill();
			}
			return false;
		}

		public override void AI()
		{
			this.Target();
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 8)
				{
					base.projectile.frame = 0;
				}
			}
			bool flag = base.projectile.type == base.mod.ProjectileType("EaglecrestMinion");
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (flag)
			{
				if (player.dead)
				{
					modPlayer.eaglecrestMinion = false;
				}
				if (modPlayer.eaglecrestMinion)
				{
					base.projectile.timeLeft = 2;
				}
			}
			if (player.dead || !player.HasBuff(base.mod.BuffType("EaglecrestMinionBuff")))
			{
				base.projectile.Kill();
			}
			float num = 700f;
			float num2 = 800f;
			Vector2 vector = base.projectile.position;
			bool flag2 = false;
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, npc.position, npc.width, npc.height))
				{
					num = Vector2.Distance(base.projectile.Center, vector);
					vector = npc.Center;
					flag2 = true;
				}
			}
			else
			{
				for (int i = 0; i < 200; i++)
				{
					NPC npc2 = Main.npc[i];
					if (npc2.CanBeChasedBy(this, false))
					{
						float num3 = Vector2.Distance(npc2.Center, base.projectile.Center);
						if ((num3 < num || !flag2) && Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, npc2.position, npc2.width, npc2.height))
						{
							num = num3;
							vector = npc2.Center;
							flag2 = true;
						}
					}
				}
			}
			float num4 = num2;
			if (flag2)
			{
				num4 = 1200f;
			}
			if (Vector2.Distance(player.Center, base.projectile.Center) > num4)
			{
				base.projectile.ai[0] = 1f;
				base.projectile.tileCollide = false;
				base.projectile.netUpdate = true;
			}
			if (flag2 && base.projectile.ai[0] == 0f)
			{
				Vector2 vector2 = vector - base.projectile.Center;
				float num5 = vector2.Length();
				vector2.Normalize();
				if (num5 > 200f)
				{
					float num6 = 6f;
					vector2 *= num6;
					base.projectile.velocity = (base.projectile.velocity * 40f + vector2) / 41f;
				}
				else
				{
					float num7 = 4f;
					vector2 *= -num7;
					base.projectile.velocity = (base.projectile.velocity * 40f + vector2) / 41f;
				}
			}
			else
			{
				bool flag3 = false;
				if (!flag3)
				{
					flag3 = (base.projectile.ai[0] == 1f);
				}
				float num8 = 6f;
				if (flag3)
				{
					num8 = 15f;
				}
				Vector2 center = base.projectile.Center;
				float num9 = (player.Center - center + new Vector2(0f, -60f)).Length();
				if (num9 <= 200f || num8 < 8f)
				{
				}
			}
			if (base.projectile.ai[1] > 0f)
			{
				base.projectile.ai[1] += (float)Main.rand.Next(1, 4);
			}
			if (base.projectile.ai[1] > 40f)
			{
				base.projectile.ai[1] = 0f;
				base.projectile.netUpdate = true;
			}
			if (base.projectile.ai[0] == 0f && flag2 && base.projectile.ai[1] == 0f)
			{
				base.projectile.ai[1] += 1f;
				if (Main.myPlayer == base.projectile.owner && Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, vector, 0, 0))
				{
					Vector2 vector3 = vector - base.projectile.Center;
					vector3.Normalize();
					vector3 *= 16f;
					int num10 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, vector3.X, vector3.Y, base.mod.ProjectileType("AncientBeam1"), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
					Main.projectile[num10].hostile = false;
					Main.projectile[num10].friendly = true;
					Main.projectile[num10].magic = false;
					Main.projectile[num10].minion = true;
					Main.projectile[num10].netUpdate = true;
					base.projectile.netUpdate = true;
				}
			}
			BaseAI.AIMinionFighter(base.projectile, ref base.projectile.ai, player, false, 20, 20, 40, 800, 900, -1f, -1f, -1f, delegate(Entity proj, Entity owner)
			{
				if (this.target != player)
				{
					return this.target;
				}
				return null;
			});
		}

		public void Target()
		{
			Vector2 center = Main.player[base.projectile.owner].Center;
			if (this.target != null && this.target != Main.player[base.projectile.owner] && !this.CanTarget(this.target, center))
			{
				this.target = null;
			}
			if (this.target == null || this.target == Main.player[base.projectile.owner])
			{
				int[] npcs = BaseAI.GetNPCs(center, -1, null, (float)this.maxDistToAttack, null);
				float num = (float)this.maxDistToAttack;
				foreach (int num2 in npcs)
				{
					NPC npc = Main.npc[num2];
					float num3 = Vector2.Distance(center, npc.Center);
					if (this.CanTarget(npc, center) && num3 < num)
					{
						this.target = npc;
						num = num3;
					}
				}
			}
			if (this.target == null)
			{
				this.target = Main.player[base.projectile.owner];
			}
		}

		public bool CanTarget(Entity codable, Vector2 startPos)
		{
			if (codable is NPC)
			{
				NPC npc = (NPC)codable;
				return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5 && Vector2.Distance(startPos, npc.Center) < (float)this.maxDistToAttack && Math.Abs(npc.Center.Y - startPos.Y) < 64f && (BaseUtility.CanHit(base.projectile.Hitbox, npc.Hitbox) || BaseUtility.CanHit(Main.player[base.projectile.owner].Hitbox, npc.Hitbox));
			}
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 4;
		}

		public override bool MinionContactDamage()
		{
			return true;
		}

		public int maxDistToAttack = 800;

		public Entity target;
	}
}
