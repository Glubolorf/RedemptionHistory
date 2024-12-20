using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Projectiles.v08;
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
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex2].velocity *= 2.6f;
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
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
			}
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			if (Main.player[base.npc.target].Center.X > base.npc.Center.X)
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
						int pieCut = 16;
						for (int i = 0; i < pieCut; i++)
						{
							int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<InfectedFlinxWave>(), 1, 3f, 255, 0f, 0f);
							Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(26f, 0f), (float)i / (float)pieCut * 6.28f);
							Main.projectile[projID].netUpdate = true;
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
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/v08/InfectedSnowFlinxHop");
			Texture2D fakeDeathAni = base.mod.GetTexture("NPCs/v08/InfectedSnowFlinxFakeDeath");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.fakeDeath)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.fakeDeath)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.fakeDeath)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = fakeDeathAni.Height / 8;
				int y7 = num215 * this.fakeDeathFrame;
				Main.spriteBatch.Draw(fakeDeathAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, fakeDeathAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)fakeDeathAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool fakeDeath;

		private int fakeDeathFrame;

		private int fakeDeathCounter;

		private int fakeDeathTimer;

		private bool hop;
	}
}
