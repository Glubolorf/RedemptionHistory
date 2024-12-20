using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class CorruptedDrone1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Attack Drone");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 104;
			base.npc.height = 56;
			base.npc.friendly = false;
			base.npc.damage = 90;
			this.aiType = 0;
			base.npc.defense = 75;
			base.npc.lifeMax = 45000;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[base.mod.BuffType("UltraFlameDebuff")] = true;
			base.npc.buffImmune[base.mod.BuffType("EnjoymentDebuff")] = true;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 25, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AttackDroneGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AttackDroneGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AttackDroneGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AttackDroneGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AttackDroneGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AttackDroneGore6"), 1f);
				for (int i = 0; i < 25; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.8f);
					Main.dust[num].velocity *= 1.8f;
				}
				for (int j = 0; j < 15; j++)
				{
					int num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 2.2f);
					Main.dust[num2].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			if (!this.change)
			{
				int num = Main.rand.Next(7);
				if (num == 0)
				{
					base.npc.SetDefaults(base.mod.NPCType("CorruptedCopter1"), -1f);
					this.change = true;
				}
				if (num >= 1)
				{
					this.change = true;
				}
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 74;
				if (base.npc.frame.Y > 74)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.Target();
			this.DespawnHandler();
			if (this.player.Center.X > base.npc.Center.X)
			{
				this.Move(new Vector2(-200f, -100f));
			}
			else
			{
				this.Move(new Vector2(200f, -100f));
			}
			base.npc.spriteDirection = base.npc.direction;
			base.npc.rotation = base.npc.velocity.X * 0.05f;
			float num2 = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num2 <= 400f && Main.rand.Next(150) == 0 && !this.minigunAttack)
			{
				this.minigunAttack = true;
			}
			if (!this.minigunAttack)
			{
				this.attackStart = false;
			}
			if (this.minigunAttack)
			{
				this.attackTimer++;
				if (this.attackTimer > 5 && this.attackTimer < 35)
				{
					this.spamTimer++;
					if (this.spamTimer == 2)
					{
						if (base.npc.direction == -1)
						{
							float num3 = 9f;
							Vector2 vector;
							vector..ctor(base.npc.position.X + 88f, base.npc.position.Y + 56f);
							int num4 = 55;
							int num5 = 110;
							float num6 = (float)Math.Atan2((double)(vector.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector.X - (this.player.position.X + (float)this.player.width * 0.5f)));
							int num7 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num6) * (double)num3 * -1.0), (float)(Math.Sin((double)num6) * (double)num3 * -1.0), num5, num4, 0f, 0, 0f, 0f);
							Main.projectile[num7].netUpdate = true;
						}
						else
						{
							float num8 = 9f;
							Vector2 vector2;
							vector2..ctor(base.npc.position.X + 16f, base.npc.position.Y + 56f);
							int num9 = 55;
							int num10 = 110;
							float num11 = (float)Math.Atan2((double)(vector2.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector2.X - (this.player.position.X + (float)this.player.width * 0.5f)));
							int num12 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num11) * (double)num8 * -1.0), (float)(Math.Sin((double)num11) * (double)num8 * -1.0), num10, num9, 0f, 0, 0f, 0f);
							Main.projectile[num12].netUpdate = true;
						}
					}
					if (this.spamTimer >= 3)
					{
						this.spamTimer = 0;
					}
				}
				if (this.attackTimer >= 35)
				{
					this.minigunAttack = false;
					this.attackTimer = 0;
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			this.speed = 15f;
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/v08/CorruptedDrone1_Glow");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * ((Main.hardMode && NPC.downedMoonlord && RedeWorld.downedVlitch3 && RedeWorld.downedPatientZero && !NPC.AnyNPCs(base.mod.NPCType("CorruptedDrone1")) && !NPC.AnyNPCs(base.mod.NPCType("CorruptedCopter1"))) ? 0.009f : 0f);
		}

		private bool minigunAttack;

		private bool attackStart;

		private int attackTimer;

		private int spamTimer;

		private Player player;

		private float speed;

		private bool change;
	}
}
