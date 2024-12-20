using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class SpaceKeeper : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Space Keeper");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = 0;
			this.aiType = 0;
			base.npc.lifeMax = 3000;
			base.npc.damage = 56;
			base.npc.defense = 20;
			base.npc.knockBackResist = 0f;
			base.npc.width = 44;
			base.npc.height = 68;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[base.mod.BuffType("UltraFlameDebuff")] = true;
			base.npc.buffImmune[base.mod.BuffType("EnjoymentDebuff")] = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit42;
			base.npc.DeathSound = SoundID.NPCDeath14;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 5; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				for (int j = 0; j < 25; j++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex2].velocity *= 1.8f;
				}
				for (int k = 0; k < 15; k++)
				{
					int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex3].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 72;
				if (base.npc.frame.Y > 220)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (this.targeted)
			{
				this.chargeCounter++;
				if (this.chargeCounter > 5)
				{
					this.chargeFrame++;
					this.chargeCounter = 0;
				}
				if (this.chargeFrame >= 4)
				{
					this.chargeFrame = 0;
				}
			}
			this.Target();
			this.DespawnHandler();
			if (this.player.Center.X > base.npc.Center.X)
			{
				this.Move(new Vector2(-150f, 0f));
			}
			else
			{
				this.Move(new Vector2(150f, 0f));
			}
			this.timer++;
			if (this.timer == 1)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 2.9f;
				}
			}
			if (this.timer >= 210)
			{
				this.targeted = true;
			}
			if (this.targeted)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				this.attackTimer++;
				if (this.attackTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityCyan, "Charging...", true, true);
				}
				if (this.attackTimer == 30)
				{
					if (RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityCyan, "Burst!", true, true);
					}
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut = 4;
						for (int j = 0; j < pieCut; j++)
						{
							int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 462, 30, 0f, 255, 0f, 0f);
							Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)j / (float)pieCut * 6.28f);
							Main.projectile[projID].netUpdate = true;
						}
					}
					else
					{
						Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut2 = 4;
						for (int k = 0; k < pieCut2; k++)
						{
							int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 462, 30, 0f, 255, 0f, 0f);
							Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)k / (float)pieCut2 * 6.28f);
							Main.projectile[projID2].netUpdate = true;
						}
					}
				}
				if (this.attackTimer == 50)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut3 = 8;
						for (int l = 0; l < pieCut3; l++)
						{
							int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 462, 30, 0f, 255, 0f, 0f);
							Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)l / (float)pieCut3 * 6.28f);
							Main.projectile[projID3].netUpdate = true;
						}
					}
					else
					{
						Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
						int pieCut4 = 8;
						for (int m = 0; m < pieCut4; m++)
						{
							int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, 462, 30, 0f, 255, 0f, 0f);
							Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)m / (float)pieCut4 * 6.28f);
							Main.projectile[projID4].netUpdate = true;
						}
					}
				}
				if (this.attackTimer > 90)
				{
					this.targeted = false;
					this.attackTimer = 0;
					this.timer = 10;
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			this.speed = 12f;
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 9f;
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D slashAni = base.mod.GetTexture("NPCs/SpaceKeeperCharge");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.targeted)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.targeted)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = slashAni.Height / 4;
				int y6 = num214 * this.chargeFrame;
				Main.spriteBatch.Draw(slashAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, slashAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)slashAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		private Player player;

		private float speed;

		public int timer;

		private bool targeted;

		private int chargeFrame;

		private int chargeCounter;

		private int attackTimer;
	}
}
