using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
{
	public class MACEProjectFist1B : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Lab/MACEProjectFist1A";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("MACE Project's Fist");
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 12000;
			base.npc.damage = 130;
			base.npc.defense = 70;
			base.npc.knockBackResist = 0f;
			base.npc.width = 64;
			base.npc.height = 62;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.npcSlots = 1f;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			base.npc.alpha = 225;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.6f;
				}
				for (int j = 0; j < 15; j++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			base.npc.spriteDirection = -1;
			if (base.npc.ai[0] < 2f)
			{
				this.Move(new Vector2(this.xPos, this.yPos));
			}
			if (base.npc.ai[0] == 0f)
			{
				this.xPos = -50f;
				this.yPos = -150f;
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] <= 120f)
				{
					base.npc.alpha -= 8;
				}
				if (base.npc.ai[1] == 120f)
				{
					base.npc.ai[1] += 150f;
				}
				if (base.npc.ai[1] >= 300f)
				{
					base.npc.ai[1] = 0f;
					base.npc.ai[0] += 1f;
					this.landed = false;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[0] == 1f)
			{
				base.npc.velocity.X = 0f;
				base.npc.noTileCollide = false;
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] < 46f)
				{
					base.npc.velocity.Y = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 46f && base.npc.ai[1] < 50f)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y - 3f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] > 50f)
				{
					NPC npc2 = base.npc;
					npc2.velocity.Y = npc2.velocity.Y + 10f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] > 90f && this.landed)
				{
					base.npc.ai[1] = 0f;
					base.npc.ai[0] += 1f;
					base.npc.noTileCollide = true;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[0] == 2f)
			{
				base.npc.ai[0] += 1f;
				this.MoveVector2 = this.PosPick();
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] == 3f)
			{
				if (base.npc.ai[2] >= 5f)
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 0f;
					base.npc.ai[1] = 150f;
					base.npc.noTileCollide = true;
					base.npc.netUpdate = true;
				}
				else if (Vector2.Distance(base.npc.Center, this.MoveVector2) < 10f)
				{
					base.npc.velocity *= 0f;
					this.landed = false;
					base.npc.noTileCollide = false;
					base.npc.netUpdate = true;
					base.npc.ai[1] = 0f;
					base.npc.ai[0] = 1f;
					base.npc.ai[2] += 1f;
				}
				else
				{
					this.MoveToVector2(this.MoveVector2);
					base.npc.noTileCollide = true;
				}
			}
			if (base.npc.collideY && base.npc.velocity.Y > 0f)
			{
				if (!this.landed)
				{
					Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int i = 0; i < 15; i++)
					{
						int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 2f);
						Main.dust[dustIndex].velocity *= 1.6f;
					}
					for (int j = 0; j < 5; j++)
					{
						int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 2f);
						Main.dust[dustIndex2].velocity *= 2.6f;
					}
					int p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(10f, 0f), ModContent.ProjectileType<MACEShock1>(), 35, 3f, 255, 0f, 0f);
					p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(10f, 0f), ModContent.ProjectileType<MACEShock2>(), 35, 3f, 255, 0f, 0f);
					p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(10f, 0f), ModContent.ProjectileType<MACEShock3>(), 35, 3f, 255, 0f, 0f);
					p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(-10f, 0f), ModContent.ProjectileType<MACEShock1>(), 35, 3f, 255, 0f, 0f);
					p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(-10f, 0f), ModContent.ProjectileType<MACEShock2>(), 35, 3f, 255, 0f, 0f);
					p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(-10f, 0f), ModContent.ProjectileType<MACEShock3>(), 35, 3f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
					this.landed = true;
					base.npc.netUpdate = true;
				}
				base.npc.velocity.X = 0f;
			}
		}

		public Vector2 PosPick()
		{
			int PosChoice = Main.rand.Next(7);
			Vector2 Pos = new Vector2(this.Origin.X + 768f, this.Origin.Y + 2576f);
			Vector2 Pos2 = new Vector2(this.Origin.X + 896f, this.Origin.Y + 2576f);
			Vector2 Pos3 = new Vector2(this.Origin.X + 1024f, this.Origin.Y + 2576f);
			Vector2 Pos4 = new Vector2(this.Origin.X + 1152f, this.Origin.Y + 2576f);
			Vector2 Pos5 = new Vector2(this.Origin.X + 1280f, this.Origin.Y + 2576f);
			Vector2 Pos6 = new Vector2(this.Origin.X + 1408f, this.Origin.Y + 2576f);
			Vector2 Pos7 = new Vector2(this.Origin.X + 1536f, this.Origin.Y + 2576f);
			if (PosChoice == 0)
			{
				return Pos;
			}
			if (PosChoice == 1)
			{
				return Pos2;
			}
			if (PosChoice == 2)
			{
				return Pos3;
			}
			if (PosChoice == 3)
			{
				return Pos4;
			}
			if (PosChoice == 4)
			{
				return Pos5;
			}
			if (PosChoice == 5)
			{
				return Pos6;
			}
			if (PosChoice == 6)
			{
				return Pos7;
			}
			return Pos;
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = 15f;
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			this.speed = 20f;
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 5f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.npc.velocity = move;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		private Player player;

		private float speed;

		private bool landed;

		private float xPos;

		private float yPos;

		public Vector2 Origin = new Vector2((float)((int)((float)Main.maxTilesX * 0.55f)), (float)((int)((float)Main.maxTilesY * 0.65f))) * 16f;

		public Vector2 MoveVector2;
	}
}
