using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class AndroidMinionPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Android Minion");
			Main.projFrames[base.projectile.type] = 6;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 30;
			base.projectile.minion = true;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
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
			if (base.projectile.velocity.Y != 0f)
			{
				base.projectile.frame = 1;
			}
			else if (base.projectile.velocity.X == 0f)
			{
				base.projectile.frame = 0;
			}
			else
			{
				if (base.projectile.frame < 2)
				{
					base.projectile.frame = 2;
				}
				Projectile projectile = base.projectile;
				int num639 = projectile.frameCounter + 1;
				projectile.frameCounter = num639;
				if (num639 >= 5)
				{
					base.projectile.frameCounter = 0;
					Projectile projectile2 = base.projectile;
					num639 = projectile2.frame + 1;
					projectile2.frame = num639;
					if (num639 >= 6)
					{
						base.projectile.frame = 2;
					}
				}
			}
			bool flag27 = base.projectile.type == ModContent.ProjectileType<AndroidMinionPro>();
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (flag27)
			{
				if (player.dead)
				{
					modPlayer.androidMinion = false;
				}
				if (modPlayer.androidMinion)
				{
					base.projectile.timeLeft = 2;
				}
			}
			if (player.dead || !player.HasBuff(ModContent.BuffType<AndroidMinionBuff>()))
			{
				base.projectile.Kill();
			}
			float num633 = 700f;
			float num634 = 800f;
			Vector2 vector46 = base.projectile.position;
			bool flag25 = false;
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, npc.position, npc.width, npc.height))
				{
					num633 = Vector2.Distance(base.projectile.Center, vector46);
					vector46 = npc.Center;
					flag25 = true;
				}
			}
			else
			{
				for (int i = 0; i < 200; i++)
				{
					NPC npc2 = Main.npc[i];
					if (npc2.CanBeChasedBy(this, false))
					{
						float num635 = Vector2.Distance(npc2.Center, base.projectile.Center);
						if ((num635 < num633 || !flag25) && Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, npc2.position, npc2.width, npc2.height))
						{
							num633 = num635;
							vector46 = npc2.Center;
							flag25 = true;
						}
					}
				}
			}
			float num636 = num634;
			if (flag25)
			{
				num636 = 1200f;
			}
			if (Vector2.Distance(player.Center, base.projectile.Center) > num636)
			{
				base.projectile.ai[0] = 1f;
				base.projectile.tileCollide = false;
				base.projectile.netUpdate = true;
			}
			if (flag25 && base.projectile.ai[0] == 0f)
			{
				Vector2 vector47 = vector46 - base.projectile.Center;
				float num640 = vector47.Length();
				vector47.Normalize();
				if (num640 > 200f)
				{
					float scaleFactor2 = 6f;
					vector47 *= scaleFactor2;
					base.projectile.velocity = (base.projectile.velocity * 40f + vector47) / 41f;
				}
				else
				{
					float num637 = 4f;
					vector47 *= -num637;
					base.projectile.velocity = (base.projectile.velocity * 40f + vector47) / 41f;
				}
			}
			else
			{
				bool flag26 = false;
				if (!flag26)
				{
					flag26 = (base.projectile.ai[0] == 1f);
				}
				float num638 = 6f;
				if (flag26)
				{
					num638 = 15f;
				}
				Vector2 center2 = base.projectile.Center;
				if ((player.Center - center2 + new Vector2(0f, -60f)).Length() <= 200f || num638 < 8f)
				{
				}
			}
			if (base.projectile.ai[1] > 0f)
			{
				base.projectile.ai[1] += (float)Main.rand.Next(1, 4);
			}
			if (base.projectile.ai[1] > 70f)
			{
				base.projectile.ai[1] = 0f;
				base.projectile.netUpdate = true;
			}
			if (base.projectile.ai[0] == 0f && flag25 && base.projectile.ai[1] == 0f)
			{
				base.projectile.ai[1] += 1f;
				if (Main.myPlayer == base.projectile.owner && Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, vector46, 0, 0))
				{
					Vector2 value19 = vector46 - base.projectile.Center;
					value19.Normalize();
					value19 *= 16f;
					int proj2 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, value19.X, value19.Y, ModContent.ProjectileType<AndroidMinionFistPro>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
					Main.projectile[proj2].netUpdate = true;
					base.projectile.netUpdate = true;
				}
			}
			BaseAI.AIMinionFighter(base.projectile, ref base.projectile.ai, player, false, 20, 20, 70, 800, 900, -1f, -1f, -1f, delegate(Entity proj, Entity owner)
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
			Vector2 startPos = Main.player[base.projectile.owner].Center;
			if (this.target != null && this.target != Main.player[base.projectile.owner] && !this.CanTarget(this.target, startPos))
			{
				this.target = null;
			}
			if (this.target == null || this.target == Main.player[base.projectile.owner])
			{
				int[] npcs = BaseAI.GetNPCs(startPos, -1, null, (float)this.maxDistToAttack, null);
				float prevDist = (float)this.maxDistToAttack;
				foreach (int i in npcs)
				{
					NPC npc = Main.npc[i];
					float dist = Vector2.Distance(startPos, npc.Center);
					if (this.CanTarget(npc, startPos) && dist < prevDist)
					{
						this.target = npc;
						prevDist = dist;
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

		public override bool MinionContactDamage()
		{
			return false;
		}

		public int maxDistToAttack = 1800;

		public Entity target;
	}
}
