using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class InfectedSnowFlinx : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Snow Flinx");
			Main.npcFrameCount[base.npc.type] = 11;
		}

		public override void SetDefaults()
		{
			base.npc.width = 52;
			base.npc.height = 44;
			base.npc.damage = 75;
			base.npc.friendly = false;
			base.npc.defense = 0;
			base.npc.lifeMax = 2040;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 500f;
			base.npc.knockBackResist = 1.1f;
			base.npc.aiStyle = 3;
			this.aiType = 185;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num].velocity *= 2.6f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff2"), Main.rand.Next(250, 500), true);
			}
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (base.npc.velocity.Y == 0f)
			{
				this.hop = false;
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 50;
					if (base.npc.frame.Y > 500)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.fakeDeath)
				{
					this.fakeDeathCounter++;
					if (this.fakeDeathCounter > 10)
					{
						this.fakeDeathFrame++;
						this.fakeDeathCounter = 0;
					}
					if (this.fakeDeathFrame >= 8)
					{
						this.fakeDeathFrame = 6;
					}
				}
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.7f) && Main.rand.Next(250) == 0 && !this.fakeDeath)
				{
					this.fakeDeath = true;
				}
				if (!this.fakeDeath)
				{
					base.npc.aiStyle = 3;
				}
				if (this.fakeDeath)
				{
					this.fakeDeathTimer++;
					base.npc.aiStyle = 0;
					base.npc.knockBackResist = 0f;
					base.npc.velocity.X = 0f;
					if (this.fakeDeathTimer >= 240)
					{
						int num = 16;
						for (int i = 0; i < num; i++)
						{
							int num2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("InfectedFlinxWave"), 1, 3f, 255, 0f, 0f);
							Main.projectile[num2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(26f, 0f), (float)i / (float)num * 6.28f);
							Main.projectile[num2].netUpdate = true;
						}
						this.fakeDeathTimer = 0;
						return;
					}
				}
			}
			else
			{
				this.hop = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/v08/InfectedSnowFlinxHop");
			Texture2D texture2 = base.mod.GetTexture("NPCs/v08/InfectedSnowFlinxFakeDeath");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.fakeDeath)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.hop && !this.fakeDeath)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
				int num2 = num * this.hopFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.fakeDeath)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 8;
				int num4 = num3 * this.fakeDeathFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool fakeDeath;

		private int fakeDeathFrame;

		private int fakeDeathCounter;

		private int fakeDeathTimer;

		private bool hop;

		private int hopFrame;

		private int hopCounter;
	}
}
