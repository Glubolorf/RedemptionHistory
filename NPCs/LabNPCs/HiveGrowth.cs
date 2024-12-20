﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	public class HiveGrowth : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hive Growth");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 15000;
			base.npc.damage = 100;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 58;
			base.npc.height = 58;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			base.npc.alpha = 225;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 100, default(Color), 2f);
					Main.dust[num].velocity *= 1.6f;
				}
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 10.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 60;
				if (base.npc.frame.Y > 120)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.Target();
			this.DespawnHandler();
			if (this.player.Center.X > base.npc.Center.X)
			{
				this.Move(new Vector2(50f, 0f));
			}
			else
			{
				this.Move(new Vector2(-50f, 0f));
			}
			this.startTimer++;
			if (base.npc.alpha > 0)
			{
				base.npc.alpha -= 20;
			}
			if (this.startTimer >= 270)
			{
				this.targeted = true;
			}
			if (this.targeted)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				this.attackTimer++;
				if (this.attackTimer == 45)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Zap1").WithVolume(0.8f).WithPitchVariance(0f), -1, -1);
					}
					int num = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("EyeFlashPro"), 40, 3f, 255, 0f, 0f);
					int num2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("EyeFlashProH"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num].netUpdate = true;
					Main.projectile[num2].netUpdate = true;
				}
				if (this.attackTimer > 90)
				{
					this.targeted = false;
					this.attackTimer = 0;
					this.smashTimer = 0;
					this.startTimer = 140;
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
				this.speed = 7f;
			}
			Vector2 vector = this.player.Center + offset;
			Vector2 vector2 = vector - base.npc.Center;
			float num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			float num2 = 30f;
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

		private int smashTimer;
	}
}
