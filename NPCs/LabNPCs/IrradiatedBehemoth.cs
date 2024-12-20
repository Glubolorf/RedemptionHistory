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
					Vector2 vector;
					vector..ctor(400f, 50f);
					base.npc.Center = base.npc.position + vector;
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
					int num = NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("SludgyBoi2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num].netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("WalterInfected")) <= 2 && Main.rand.Next(350) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num2 = NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("WalterInfected"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num2].netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("SludgyBlob")) <= 3 && Main.rand.Next(155) == 0)
				{
					int num3 = NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 96, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num3].netUpdate = true;
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
							int num4 = Dust.NewDust(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 88f), 4, 4, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
							Main.dust[num4].velocity *= 1.9f;
						}
						int num5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2((float)(-6 + Main.rand.Next(-18, 0)), (float)(-2 + Main.rand.Next(0, 4))), base.mod.ProjectileType("GloopBallPro1"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num5].netUpdate = true;
					}
					if (base.npc.ai[2] >= 4f)
					{
						int num6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2((float)(-6 + Main.rand.Next(-16, 0)), (float)(-2 + Main.rand.Next(0, 8))), base.mod.ProjectileType("GreenGloopPro2"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num6].netUpdate = true;
						base.npc.ai[2] = 2f;
					}
				}
				if (base.npc.ai[1] == 350f)
				{
					int num7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 0f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int num8 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int num9 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num7].netUpdate = true;
					Main.projectile[num8].netUpdate = true;
					Main.projectile[num9].netUpdate = true;
				}
				if (base.npc.ai[1] == 410f)
				{
					int num10 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 1f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int num11 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -1f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num10].netUpdate = true;
					Main.projectile[num11].netUpdate = true;
				}
				if (base.npc.ai[1] == 470f)
				{
					int num12 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 0f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int num13 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, 2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					int num14 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 58f, base.npc.position.Y + 96f), new Vector2(-5f, -2f), base.mod.ProjectileType("GreenGasPro2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num12].netUpdate = true;
					Main.projectile[num13].netUpdate = true;
					Main.projectile[num14].netUpdate = true;
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
							int num15 = Dust.NewDust(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 88f), 4, 4, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
							Main.dust[num15].velocity *= 1.9f;
						}
						float num16 = 8f;
						Vector2 vector2;
						vector2..ctor(base.npc.position.X + 58f, base.npc.position.Y + 96f);
						int num17 = 40;
						int num18 = base.mod.ProjectileType("GreenGloopPro3");
						float num19 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
						int num20 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num19) * (double)num16 * -1.0), (float)(Math.Sin((double)num19) * (double)num16 * -1.0), num18, num17, 0f, 0, 0f, 0f);
						Main.projectile[num20].netUpdate = true;
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
