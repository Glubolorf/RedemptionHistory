using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	[AutoloadBossHead]
	public class OmegaOblitDamaged : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Obliterator");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = 0;
			this.aiType = 0;
			base.npc.lifeMax = 65500;
			base.npc.damage = 280;
			base.npc.defense = 20;
			base.npc.knockBackResist = 0f;
			base.npc.width = 112;
			base.npc.height = 178;
			base.npc.value = (float)Item.buyPrice(0, 25, 0, 0);
			base.npc.npcSlots = 2f;
			base.npc.boss = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[base.mod.BuffType("UltraFlameDebuff")] = true;
			base.npc.buffImmune[base.mod.BuffType("EnjoymentDebuff")] = true;
			base.npc.netAlways = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit42;
			base.npc.DeathSound = SoundID.NPCDeath14;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossVlitch2");
			this.bossBag = base.mod.ItemType("OmegaOblitBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/OmegaGore1"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/OmegaGore2"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/OmegaGore3"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/OmegaGore4"), 1f);
				for (int i = 0; i < 120; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.7f);
					Main.dust[num].velocity *= 1.9f;
				}
				for (int j = 0; j < 45; j++)
				{
					int num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num2].velocity *= 1.8f;
				}
				for (int k = 0; k < 25; k++)
				{
					int num3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num3].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.5f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.2f);
			base.npc.defense = base.npc.defense + numPlayers;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			RedeWorld.downedVlitch3 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk3 && !NPC.AnyNPCs(base.mod.NPCType("VlitchWormHead")) && !NPC.AnyNPCs(base.mod.NPCType("VlitchCleaver")))
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), base.mod.ProjectileType("GirusTalking3"), 0, 0f, 255, 0f, 0f);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PlasmaJawser"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("OmegaClaw"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GloopContainer"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(14) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusMask"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(25, 35), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchBattery"), Main.rand.Next(3, 5), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("OblitBrain"), 1, false, 0, false, false);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (Main.rand.Next(10) == 0)
			{
				int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].velocity *= 1.1f;
			}
			if (!this.charging)
			{
				this.Move(new Vector2(-340f, 0f));
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 15.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 158;
				if (base.npc.frame.Y > 316)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (this.attackMode == 0)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				this.aiCounter++;
				base.npc.dontTakeDamage = true;
			}
			if (this.attackMode == 1)
			{
				if (base.npc.life > 35500)
				{
					if (Main.rand.Next(250) == 0)
					{
						for (int i = 0; i < 20; i++)
						{
							int num2 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num2].velocity *= 1.9f;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					}
					if (Main.rand.Next(250) == 0)
					{
						Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
					}
					if (Main.rand.Next(250) == 0)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
					}
				}
				if (base.npc.life <= 35500)
				{
					if (Main.rand.Next(150) == 0)
					{
						for (int j = 0; j < 20; j++)
						{
							int num3 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num3].velocity *= 1.9f;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					}
					if (Main.rand.Next(200) == 0)
					{
						Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
					}
					if (Main.rand.Next(50) == 0)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
					}
				}
				this.timer++;
				if (this.timer <= 155)
				{
					this.charging = true;
				}
				if (this.timer > 155)
				{
					this.charging = false;
				}
				if (this.timer == 5)
				{
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num4 = (float)Math.Atan2((double)(vector.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num4) * 26.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num4) * 26.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color = default(Color);
					Rectangle rectangle;
					rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num5 = 30;
					for (int k = 1; k <= num5; k++)
					{
						int num6 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 235, 0f, 0f, 100, color, 2.5f);
						Main.dust[num6].noGravity = false;
					}
					return;
				}
				if (this.timer == 35)
				{
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num7 = (float)Math.Atan2((double)(vector2.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector2.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num7) * 32.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num7) * 32.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color2 = default(Color);
					Rectangle rectangle2;
					rectangle2..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num8 = 30;
					for (int l = 1; l <= num8; l++)
					{
						int num9 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 235, 0f, 0f, 100, color2, 2.5f);
						Main.dust[num9].noGravity = false;
					}
					return;
				}
				if (this.timer == 65)
				{
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num10 = (float)Math.Atan2((double)(vector3.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector3.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num10) * 38.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num10) * 32.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color3 = default(Color);
					Rectangle rectangle3;
					rectangle3..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num11 = 30;
					for (int m = 1; m <= num11; m++)
					{
						int num12 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 235, 0f, 0f, 100, color3, 2.5f);
						Main.dust[num12].noGravity = false;
					}
					return;
				}
				if (this.timer == 95)
				{
					Vector2 vector4;
					vector4..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num13 = (float)Math.Atan2((double)(vector4.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector4.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num13) * 42.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num13) * 36.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color4 = default(Color);
					Rectangle rectangle4;
					rectangle4..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num14 = 30;
					for (int n = 1; n <= num14; n++)
					{
						int num15 = Dust.NewDust(base.npc.position, rectangle4.Width, rectangle4.Height, 235, 0f, 0f, 100, color4, 2.5f);
						Main.dust[num15].noGravity = false;
					}
					return;
				}
				if (this.timer == 125)
				{
					Vector2 vector5;
					vector5..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num16 = (float)Math.Atan2((double)(vector5.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector5.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num16) * 26.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num16) * 26.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color5 = default(Color);
					Rectangle rectangle5;
					rectangle5..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num17 = 30;
					for (int num18 = 1; num18 <= num17; num18++)
					{
						int num19 = Dust.NewDust(base.npc.position, rectangle5.Width, rectangle5.Height, 235, 0f, 0f, 100, color5, 2.5f);
						Main.dust[num19].noGravity = false;
					}
					return;
				}
				if (this.timer == 205)
				{
					for (int num20 = 0; num20 < 20; num20++)
					{
						int num21 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num21].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
				}
				if (this.timer == 235)
				{
					for (int num22 = 0; num22 < 20; num22++)
					{
						int num23 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num23].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
				}
				if (this.timer == 265)
				{
					for (int num24 = 0; num24 < 20; num24++)
					{
						int num25 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num25].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
				}
				if (this.timer == 305)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(-8f, 0f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(-6f, -6f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(0f, -8f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(6f, -6f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(8f, 0f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(6f, 6f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(0f, 8f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(-6f, 6f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
				}
				if (this.timer == 310)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(-10f, 0f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(-8f, -8f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(0f, -10f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(8f, -8f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(10f, 0f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(8f, 8f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(0f, 10f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 50f), new Vector2(-8f, 8f), base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
				}
				if (this.timer == 320)
				{
					for (int num26 = 0; num26 < 20; num26++)
					{
						int num27 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num27].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 322)
				{
					for (int num28 = 0; num28 < 20; num28++)
					{
						int num29 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num29].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(5f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 325)
				{
					for (int num30 = 0; num30 < 20; num30++)
					{
						int num31 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num31].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-5f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 326)
				{
					for (int num32 = 0; num32 < 20; num32++)
					{
						int num33 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num33].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-2f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 328)
				{
					for (int num34 = 0; num34 < 20; num34++)
					{
						int num35 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num35].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(7f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 331)
				{
					for (int num36 = 0; num36 < 20; num36++)
					{
						int num37 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num37].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(1f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 332)
				{
					for (int num38 = 0; num38 < 20; num38++)
					{
						int num39 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num39].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-8f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 335)
				{
					for (int num40 = 0; num40 < 20; num40++)
					{
						int num41 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num41].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(6f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 336)
				{
					for (int num42 = 0; num42 < 20; num42++)
					{
						int num43 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num43].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(2f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer == 340)
				{
					for (int num44 = 0; num44 < 20; num44++)
					{
						int num45 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num45].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
				}
				if (this.timer >= 400)
				{
					this.timer = 0;
				}
			}
			if (this.aiCounter == 1)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int num46 = 0; num46 < 100; num46++)
				{
					int num47 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num47].velocity *= 1.9f;
				}
				for (int num48 = 0; num48 < 50; num48++)
				{
					int num49 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num49].velocity *= 1.9f;
				}
				for (int num50 = 0; num50 < 50; num50++)
				{
					int num51 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num51].velocity *= 1.9f;
				}
			}
			if (this.aiCounter == 40)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int num52 = 0; num52 < 100; num52++)
				{
					int num53 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num53].velocity *= 1.9f;
				}
				for (int num54 = 0; num54 < 50; num54++)
				{
					int num55 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num55].velocity *= 1.9f;
				}
				for (int num56 = 0; num56 < 50; num56++)
				{
					int num57 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num57].velocity *= 1.9f;
				}
			}
			if (this.aiCounter == 70)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int num58 = 0; num58 < 100; num58++)
				{
					int num59 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num59].velocity *= 1.9f;
				}
				for (int num60 = 0; num60 < 50; num60++)
				{
					int num61 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num61].velocity *= 1.9f;
				}
				for (int num62 = 0; num62 < 50; num62++)
				{
					int num63 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num63].velocity *= 1.9f;
				}
			}
			if (this.aiCounter == 1)
			{
				string text = "ERROR: POWER OVERLOAD...";
				Color rarityRed = Colors.RarityRed;
				byte r = rarityRed.R;
				rarityRed = Colors.RarityRed;
				byte g = rarityRed.G;
				rarityRed = Colors.RarityRed;
				Main.NewText(text, r, g, rarityRed.B, false);
			}
			if (this.aiCounter == 130)
			{
				string text2 = "I REQUIRE ASSISTANCE, GIRUS...";
				Color rarityRed = Colors.RarityRed;
				byte r2 = rarityRed.R;
				rarityRed = Colors.RarityRed;
				byte g2 = rarityRed.G;
				rarityRed = Colors.RarityRed;
				Main.NewText(text2, r2, g2, rarityRed.B, false);
			}
			if (this.aiCounter == 250)
			{
				this.attackMode = 1;
				base.npc.dontTakeDamage = false;
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			this.speed = 12f;
			Vector2 vector = this.player.Center + offset;
			Vector2 vector2 = vector - base.npc.Center;
			float num = this.Magnitude(vector2);
			if (num > this.speed)
			{
				vector2 *= this.speed / num;
			}
			float num2 = 10f;
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
					this.deadTimer++;
					if (this.deadTimer == 2)
					{
						string text = "TARGET OBLITERATED... RETURNING TO GIRUS...";
						Color rarityRed = Colors.RarityRed;
						byte r = rarityRed.R;
						Color rarityRed2 = Colors.RarityRed;
						byte g = rarityRed2.G;
						Color rarityRed3 = Colors.RarityRed;
						Main.NewText(text, r, g, rarityRed3.B, false);
					}
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

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		private Player player;

		private float speed;

		public int timer;

		private bool charging;

		private bool start;

		private bool beginAttacks;

		private int attackMode;

		private int aiCounter;

		private int deadTimer;

		private int attack2Timer;

		private int attack3Timer;

		private int attack4Timer;

		private int attack5Timer;
	}
}
