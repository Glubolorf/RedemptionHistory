using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.NPCs.Bosses.OmegaOblit;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class CorruptedCopter1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Attack Hovercopter");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 184;
			base.npc.height = 100;
			base.npc.friendly = false;
			base.npc.damage = 120;
			this.aiType = 0;
			base.npc.defense = 75;
			base.npc.lifeMax = 85000;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[ModContent.BuffType<UltraFlameDebuff>()] = true;
			base.npc.buffImmune[ModContent.BuffType<EnjoymentDebuff>()] = true;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 40, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				if (base.npc.direction == -1)
				{
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<AttackHovercopterPro>(), 50, 3f, 255, 0f, 0f);
				}
				else
				{
					Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<AttackHovercopterPro2>(), 50, 3f, 255, 0f, 0f);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<OmegaPilotDroid>(), 0, 0f, 0f, 0f, 0f, 255);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AttackHovercopterGore6"), 1f);
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 102;
				if (base.npc.frame.Y > 102)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.Target();
			this.DespawnHandler();
			if (this.player.Center.X > base.npc.Center.X)
			{
				this.Move(new Vector2(-400f, -100f));
			}
			else
			{
				this.Move(new Vector2(400f, -100f));
			}
			base.npc.spriteDirection = base.npc.direction;
			base.npc.rotation = base.npc.velocity.X * 0.05f;
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 600f && Main.rand.Next(150) == 0 && !this.minigunAttack)
			{
				this.minigunAttack = true;
			}
			if (Main.rand.Next(400) == 0)
			{
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), ModContent.ProjectileType<OmegaMissile>(), 60, 3f, 255, 0f, 0f);
				Main.projectile[p].netUpdate = true;
			}
			if (this.minigunAttack)
			{
				this.attackTimer++;
				if (this.attackTimer > 5 && this.attackTimer < 45)
				{
					this.spamTimer++;
					if (this.spamTimer == 2)
					{
						if (base.npc.direction == -1)
						{
							float Speed = 9f;
							Vector2 vector8 = new Vector2(base.npc.position.X + 156f, base.npc.position.Y + 82f);
							int damage = 55;
							int type = 110;
							float rotation = (float)Math.Atan2((double)(vector8.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector8.X - (this.player.position.X + (float)this.player.width * 0.5f)));
							int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
							Main.projectile[num54].netUpdate = true;
						}
						else
						{
							float Speed2 = 9f;
							Vector2 vector9 = new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 82f);
							int damage2 = 55;
							int type2 = 110;
							float rotation2 = (float)Math.Atan2((double)(vector9.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector9.X - (this.player.position.X + (float)this.player.width * 0.5f)));
							int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
							Main.projectile[num55].netUpdate = true;
						}
					}
					if (this.spamTimer >= 3)
					{
						this.spamTimer = 0;
					}
				}
				if (this.attackTimer >= 45)
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
			this.speed = 17f;
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

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/v08/CorruptedCopter1_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		private bool minigunAttack;

		private int attackTimer;

		private int spamTimer;

		private Player player;

		private float speed;
	}
}
