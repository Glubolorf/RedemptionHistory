using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	public class MACEProjectFist1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("MACE Project's Fist");
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 20000;
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
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.6f;
				}
				for (int j = 0; j < 15; j++)
				{
					int num2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num2].velocity *= 2.6f;
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
			this.Move(new Vector2(0f, -150f));
			this.startTimer++;
			if (this.startTimer <= 120)
			{
				base.npc.alpha -= 8;
			}
			if (this.startTimer >= 300)
			{
				this.targeted = true;
			}
			if (this.targeted && !this.fistSmash)
			{
				base.npc.velocity.X = 0f;
				this.attackTimer++;
				if (this.attackTimer <= 50)
				{
					base.npc.velocity.Y = 0f;
				}
				if (this.attackTimer > 50)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y + 20f;
				}
				if (this.attackTimer > 54)
				{
					base.npc.noTileCollide = false;
					this.fistSmash = true;
					base.npc.velocity.Y = 0f;
				}
			}
			if (this.fistSmash)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				this.smashTimer++;
				if (this.smashTimer == 1)
				{
					Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
					for (int i = 0; i < 15; i++)
					{
						int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num].velocity *= 1.6f;
					}
					for (int j = 0; j < 5; j++)
					{
						int num2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num2].velocity *= 2.6f;
					}
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(10f, 0f), base.mod.ProjectileType("MACEShock1"), 50, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(10f, 0f), base.mod.ProjectileType("MACEShock2"), 50, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(10f, 0f), base.mod.ProjectileType("MACEShock3"), 50, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(-10f, 0f), base.mod.ProjectileType("MACEShock1"), 50, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(-10f, 0f), base.mod.ProjectileType("MACEShock2"), 50, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 31f), new Vector2(-10f, 0f), base.mod.ProjectileType("MACEShock3"), 50, 3f, 255, 0f, 0f);
				}
				if (this.smashTimer >= 120)
				{
					this.targeted = false;
					this.fistSmash = false;
					this.attackTimer = 0;
					this.smashTimer = 0;
					this.startTimer = 150;
					base.npc.noTileCollide = true;
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			if (!this.targeted)
			{
				this.speed = 20f;
			}
			Vector2 vector = this.player.Center + offset;
			Vector2 vector2 = vector - base.npc.Center;
			float num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			float num2 = 5f;
			vector2 = (base.npc.velocity * num2 + vector2) / (num2 + 1f);
			num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			base.npc.velocity = vector2;
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
				}
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		private Player player;

		private float speed;

		private bool targeted;

		private int startTimer;

		private int attackTimer;

		private bool fistSmash;

		private int smashTimer;
	}
}
