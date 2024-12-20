﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	[AutoloadBossHead]
	public class Blisterface : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blisterface");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.width = 96;
			base.npc.height = 64;
			base.npc.friendly = false;
			base.npc.damage = 190;
			base.npc.defense = 10;
			base.npc.lifeMax = 32520;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = (float)Item.buyPrice(0, 10, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = false;
			base.npc.aiStyle = 16;
			this.aiType = 58;
			this.animationType = 58;
			base.npc.alpha = 255;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/LabBossMusic");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			RedeWorld.downedBlisterface = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Keycard2"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("RawXenium"), Main.rand.Next(42, 62), false, 0, false, false);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 30.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 68;
				if (base.npc.frame.Y > 476)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.blisterTimer++;
			if (this.blisterTimer == 1)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				if (Main.netMode != 1)
				{
					Vector2 vector;
					vector..ctor(50f, 250f);
					base.npc.Center = base.npc.position + vector;
					base.npc.netUpdate = true;
				}
			}
			if (this.blisterTimer <= 120)
			{
				base.npc.aiStyle = -1;
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.alpha -= 4;
				base.npc.dontTakeDamage = true;
			}
			if (this.blisterTimer > 120)
			{
				base.npc.aiStyle = 16;
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
			}
			if (this.beginFight)
			{
				this.fightTimer++;
				if (base.npc.life > 15000)
				{
					if (this.fightTimer == 320)
					{
						this.jumpAttack = true;
					}
				}
				else if (this.fightTimer == 170)
				{
					this.jumpAttack = true;
				}
				if (!this.jumpAttack)
				{
					base.npc.noTileCollide = false;
					if (Main.rand.Next(75) == 0)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 34f, base.npc.position.Y + 22f), new Vector2(0f, 0f), base.mod.ProjectileType("BlisterBubblePro1"), 50, 3f, 255, 0f, 0f);
					}
					if (Main.rand.Next(75) == 0)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 72f, base.npc.position.Y + 18f), new Vector2(0f, 0f), base.mod.ProjectileType("BlisterBubblePro1"), 50, 3f, 255, 0f, 0f);
					}
					if (Main.rand.Next(75) == 0)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 52f, base.npc.position.Y + 32f), new Vector2(0f, 0f), base.mod.ProjectileType("BlisterBubblePro1"), 50, 3f, 255, 0f, 0f);
					}
					if (NPC.CountNPCS(base.mod.NPCType("Blisterling2")) <= 6 && Main.rand.Next(250) == 0)
					{
						NPC.NewNPC((int)base.npc.position.X + 66, (int)base.npc.position.Y + 36, base.mod.NPCType("Blisterling2"), 0, 0f, 0f, 0f, 0f, 255);
					}
				}
				if (this.jumpAttack)
				{
					this.jumpTimer++;
					base.npc.noTileCollide = true;
					base.npc.velocity.X = 0f;
					if (this.jumpTimer == 1)
					{
						Vector2 vector2;
						vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
						Math.Atan2((double)(vector2.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector2.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
						base.npc.velocity.X = 0f;
						base.npc.velocity.Y = -15f;
						base.npc.ai[0] %= 6.2831855f;
						new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
						Color color = default(Color);
						Rectangle rectangle;
						rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
						int num = 30;
						for (int i = 1; i <= num; i++)
						{
							int num2 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 273, 0f, 0f, 100, color, 2.5f);
							Main.dust[num2].noGravity = false;
						}
						return;
					}
					if (this.jumpTimer >= 50 && this.jumpTimer < 70)
					{
						this.spamTimer++;
						if (this.spamTimer == 1)
						{
							Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
						}
						if (this.spamTimer >= 2)
						{
							if (base.npc.direction == -1)
							{
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 78f, base.npc.position.Y + 34f), new Vector2((float)(-6 + Main.rand.Next(-6, 0)), (float)(-2 + Main.rand.Next(0, 4))), base.mod.ProjectileType("BlisterBubblePro2"), 50, 3f, 255, 0f, 0f);
							}
							else
							{
								Projectile.NewProjectile(new Vector2(base.npc.position.X + 24f, base.npc.position.Y + 34f), new Vector2((float)(6 + Main.rand.Next(0, 6)), (float)(-2 + Main.rand.Next(0, 4))), base.mod.ProjectileType("BlisterBubblePro2"), 50, 3f, 255, 0f, 0f);
							}
							this.spamTimer = 0;
						}
					}
					if (this.jumpTimer >= 68)
					{
						NPC npc2 = base.npc;
						npc2.velocity.Y = npc2.velocity.Y + 0.15f;
					}
					if (this.jumpTimer >= 180 && base.npc.wet)
					{
						this.fightTimer = 0;
						this.jumpAttack = false;
						this.jumpTimer = 0;
					}
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
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

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.beginFight;
		}

		private int blisterTimer;

		private bool beginFight;

		private int fightTimer;

		private int swimTimer;

		private bool jumpAttack;

		private int jumpTimer;

		private int spamTimer;
	}
}