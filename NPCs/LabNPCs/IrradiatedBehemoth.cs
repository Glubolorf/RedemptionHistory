using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	[AutoloadBossHead]
	public class IrradiatedBehemoth : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Irradiated Behemoth");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 174;
			base.npc.height = 158;
			base.npc.friendly = false;
			base.npc.damage = 90;
			base.npc.defense = 0;
			base.npc.lifeMax = 30000;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = (float)Item.buyPrice(0, 10, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/LabBossMusic");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 200; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 3f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			RedeWorld.downedIBehemoth = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 20.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 160;
				if (base.npc.frame.Y > 480)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			this.behemothTimer++;
			if (this.behemothTimer == 1)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				if (Main.netMode != 1)
				{
					Vector2 vector;
					vector..ctor(400f, 50f);
					base.npc.Center = base.npc.position + vector;
					base.npc.netUpdate = true;
				}
			}
			if (this.behemothTimer <= 120)
			{
				base.npc.alpha -= 4;
				base.npc.dontTakeDamage = true;
			}
			if (this.behemothTimer > 120)
			{
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
			}
			if (this.beginFight)
			{
				if (NPC.CountNPCS(base.mod.NPCType("SludgyBoi2")) <= 2 && Main.rand.Next(350) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("SludgyBoi2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (NPC.CountNPCS(base.mod.NPCType("WalterInfected")) <= 2 && Main.rand.Next(350) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("WalterInfected"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (NPC.CountNPCS(base.mod.NPCType("SludgyBlob")) <= 3 && Main.rand.Next(155) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
				}
				this.fightTimer++;
				if (this.fightTimer == 50)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.fightTimer >= 150 && this.fightTimer < 200)
				{
					this.spamTimer++;
					if (this.spamTimer == 1)
					{
						Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					if (this.spamTimer == 3)
					{
						for (int i = 0; i < 10; i++)
						{
							int num = Dust.NewDust(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 88f), 4, 4, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
							Main.dust[num].velocity *= 1.9f;
						}
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2((float)(-6 + Main.rand.Next(-18, 0)), (float)(-2 + Main.rand.Next(0, 4))), base.mod.ProjectileType("GloopBallPro1"), 40, 3f, 255, 0f, 0f);
					}
					if (this.spamTimer >= 4)
					{
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2((float)(-6 + Main.rand.Next(-16, 0)), (float)(-2 + Main.rand.Next(0, 8))), base.mod.ProjectileType("GreenGloopPro2"), 40, 3f, 255, 0f, 0f);
						this.spamTimer = 2;
					}
				}
				if (this.fightTimer == 350)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 0f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 410)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 1f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -1f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 470)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 0f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer >= 580 && this.fightTimer <= 600)
				{
					this.spamTimer++;
					if (this.spamTimer == 1)
					{
						Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					if (this.spamTimer >= 4)
					{
						for (int j = 0; j < 10; j++)
						{
							int num2 = Dust.NewDust(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 88f), 4, 4, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
							Main.dust[num2].velocity *= 1.9f;
						}
						float num3 = 8f;
						Vector2 vector2;
						vector2..ctor(base.npc.position.X + 58f, base.npc.position.Y + 96f);
						int num4 = 40;
						int num5 = base.mod.ProjectileType("GreenGloopPro3");
						float num6 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
						Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num6) * (double)num3 * -1.0), (float)(Math.Sin((double)num6) * (double)num3 * -1.0), num5, num4, 0f, 0, 0f, 0f);
						this.spamTimer = 2;
					}
				}
				if (this.fightTimer >= 660)
				{
					this.fightTimer = 0;
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

		private int behemothTimer;

		private bool beginFight;

		private int fightTimer;

		private int spamTimer;
	}
}
