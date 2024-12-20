using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Dusts;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
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
			base.npc.knockBackResist = 0.1f;
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
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 1.6f;
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
					int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<EyeFlashPro>(), 40, 3f, 255, 0f, 0f);
					int p2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<EyeFlashProH>(), 40, 3f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
					Main.projectile[p2].netUpdate = true;
				}
				if (this.attackTimer > 90)
				{
					this.targeted = false;
					this.attackTimer = 0;
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
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 30f;
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

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || Main.expertMode)
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
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
	}
}
