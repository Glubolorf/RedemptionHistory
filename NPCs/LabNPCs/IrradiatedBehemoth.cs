using System;
using System.IO;
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
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 1.9f;
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

		public override void NPCLoot()
		{
			if (!RedeWorld.labAccess3)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel3"), 1, false, 0, false, false);
			}
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.beginFight);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.beginFight = reader.ReadBool();
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
			base.npc.ai[0] += 1f;
			if (base.npc.ai[0] == 1f)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				if (Main.netMode != 1)
				{
					Vector2 newPos = new Vector2(400f, 50f);
					base.npc.Center = base.npc.position + newPos;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[0] <= 120f)
			{
				base.npc.alpha -= 4;
				base.npc.dontTakeDamage = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] > 120f)
			{
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
				base.npc.netUpdate = true;
			}
			if (this.beginFight)
			{
				if (NPC.CountNPCS(base.mod.NPCType("SludgyBoi2")) <= 2 && Main.rand.Next(350) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion = NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("SludgyBoi2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion].netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("WalterInfected")) <= 2 && Main.rand.Next(350) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion2 = NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("WalterInfected"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion2].netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("SludgyBlob")) <= 3 && Main.rand.Next(155) == 0)
				{
					int minion3 = NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion3].netUpdate = true;
				}
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 50f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (base.npc.ai[1] >= 150f && base.npc.ai[1] < 200f)
				{
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 1f)
					{
						Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					if (base.npc.ai[2] == 3f)
					{
						for (int i = 0; i < 10; i++)
						{
							int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 88f), 4, 4, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
							Main.dust[dustIndex].velocity *= 1.9f;
						}
						int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2((float)(-6 + Main.rand.Next(-18, 0)), (float)(-2 + Main.rand.Next(0, 4))), base.mod.ProjectileType("GloopBallPro1"), 40, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
					}
					if (base.npc.ai[2] >= 4f)
					{
						int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2((float)(-6 + Main.rand.Next(-16, 0)), (float)(-2 + Main.rand.Next(0, 8))), base.mod.ProjectileType("GreenGloopPro2"), 40, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
						base.npc.ai[2] = 2f;
					}
				}
				if (base.npc.ai[1] == 350f)
				{
					int p3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 0f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int p5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p3].netUpdate = true;
					Main.projectile[p4].netUpdate = true;
					Main.projectile[p5].netUpdate = true;
				}
				if (base.npc.ai[1] == 410f)
				{
					int p6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 1f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int p7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -1f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p6].netUpdate = true;
					Main.projectile[p7].netUpdate = true;
				}
				if (base.npc.ai[1] == 470f)
				{
					int p8 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 0f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int p9 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int p10 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[p8].netUpdate = true;
					Main.projectile[p9].netUpdate = true;
					Main.projectile[p10].netUpdate = true;
				}
				if (base.npc.ai[1] >= 580f && base.npc.ai[1] <= 600f)
				{
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 1f)
					{
						Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					if (base.npc.ai[2] >= 4f)
					{
						for (int j = 0; j < 10; j++)
						{
							int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 88f), 4, 4, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
							Main.dust[dustIndex2].velocity *= 1.9f;
						}
						float Speed = 8f;
						Vector2 vector8 = new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f);
						int damage = 40;
						int type = base.mod.ProjectileType("GreenGloopPro3");
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
						base.npc.ai[2] = 2f;
					}
				}
				if (base.npc.ai[1] >= 660f)
				{
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
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

		private bool beginFight;
	}
}
