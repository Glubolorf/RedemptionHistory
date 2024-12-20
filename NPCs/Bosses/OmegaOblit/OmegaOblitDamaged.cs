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
			if (!RedeWorld.downedVlitch3)
			{
				RedeWorld.redemptionPoints++;
				CombatText.NewText(this.player.getRect(), Color.Gold, "+1", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						for (int j = 0; j < player.inventory.Length; j++)
						{
							if (player.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Good job! All Vlitch Overlords have been... Wait...", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
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

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.9;
			return true;
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
			if (base.npc.ai[0] == 0f)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.ai[1] += 1f;
				base.npc.dontTakeDamage = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] == 1f)
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
						int num3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
						Main.projectile[num3].netUpdate = true;
					}
					if (Main.rand.Next(250) == 0)
					{
						Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num4 = Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num4].netUpdate = true;
					}
					if (Main.rand.Next(250) == 0)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num5 = Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num5].netUpdate = true;
					}
				}
				if (base.npc.life <= 35500)
				{
					if (Main.rand.Next(150) == 0)
					{
						for (int j = 0; j < 20; j++)
						{
							int num6 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num6].velocity *= 1.9f;
						}
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
						Main.projectile[num7].netUpdate = true;
					}
					if (Main.rand.Next(200) == 0)
					{
						Main.PlaySound(SoundID.Item92, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num8 = Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaPlasmaBall"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num8].netUpdate = true;
					}
					if (Main.rand.Next(50) == 0)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num9 = Projectile.NewProjectile(base.npc.position.X + 30f, base.npc.position.Y + 62f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("OmegaBlast"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num9].netUpdate = true;
					}
				}
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] <= 155f)
				{
					this.charging = true;
				}
				if (base.npc.ai[2] > 155f)
				{
					this.charging = false;
				}
				if (base.npc.ai[2] == 5f)
				{
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num10 = (float)Math.Atan2((double)(vector.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num10) * 26.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num10) * 26.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color = default(Color);
					Rectangle rectangle;
					rectangle..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num11 = 30;
					for (int k = 1; k <= num11; k++)
					{
						int num12 = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 235, 0f, 0f, 100, color, 2.5f);
						Main.dust[num12].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 35f)
				{
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num13 = (float)Math.Atan2((double)(vector2.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector2.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num13) * 32.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num13) * 28.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color2 = default(Color);
					Rectangle rectangle2;
					rectangle2..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num14 = 30;
					for (int l = 1; l <= num14; l++)
					{
						int num15 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 235, 0f, 0f, 100, color2, 2.5f);
						Main.dust[num15].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 65f)
				{
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num16 = (float)Math.Atan2((double)(vector3.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector3.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num16) * 38.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num16) * 30.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color3 = default(Color);
					Rectangle rectangle3;
					rectangle3..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num17 = 30;
					for (int m = 1; m <= num17; m++)
					{
						int num18 = Dust.NewDust(base.npc.position, rectangle3.Width, rectangle3.Height, 235, 0f, 0f, 100, color3, 2.5f);
						Main.dust[num18].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 95f)
				{
					Vector2 vector4;
					vector4..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num19 = (float)Math.Atan2((double)(vector4.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector4.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num19) * 42.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num19) * 32.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color4 = default(Color);
					Rectangle rectangle4;
					rectangle4..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num20 = 30;
					for (int n = 1; n <= num20; n++)
					{
						int num21 = Dust.NewDust(base.npc.position, rectangle4.Width, rectangle4.Height, 235, 0f, 0f, 100, color4, 2.5f);
						Main.dust[num21].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 125f)
				{
					Vector2 vector5;
					vector5..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float num22 = (float)Math.Atan2((double)(vector5.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector5.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)num22) * 26.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)num22) * 20.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color5 = default(Color);
					Rectangle rectangle5;
					rectangle5..ctor((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int num23 = 30;
					for (int num24 = 1; num24 <= num23; num24++)
					{
						int num25 = Dust.NewDust(base.npc.position, rectangle5.Width, rectangle5.Height, 235, 0f, 0f, 100, color5, 2.5f);
						Main.dust[num25].noGravity = false;
					}
					base.npc.netUpdate = true;
					return;
				}
				if (base.npc.ai[2] == 205f)
				{
					for (int num26 = 0; num26 < 20; num26++)
					{
						int num27 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num27].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num28 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
					Main.projectile[num28].netUpdate = true;
				}
				if (base.npc.ai[2] == 235f)
				{
					for (int num29 = 0; num29 < 20; num29++)
					{
						int num30 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num30].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num31 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
					Main.projectile[num31].netUpdate = true;
				}
				if (base.npc.ai[2] == 265f)
				{
					for (int num32 = 0; num32 < 20; num32++)
					{
						int num33 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num33].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num34 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 70, 3f, 255, 0f, 0f);
					Main.projectile[num34].netUpdate = true;
				}
				if (base.npc.ai[2] == 305f || base.npc.ai[2] == 310f)
				{
					int num35 = 8;
					for (int num36 = 0; num36 < num35; num36++)
					{
						int num37 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("OmegaBlast"), 70, 3f, 255, 0f, 0f);
						Main.projectile[num37].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)num36 / (float)num35 * 6.28f);
						Main.projectile[num37].netUpdate = true;
					}
				}
				if (base.npc.ai[2] == 320f)
				{
					for (int num38 = 0; num38 < 20; num38++)
					{
						int num39 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num39].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num40 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num40].netUpdate = true;
				}
				if (base.npc.ai[2] == 322f)
				{
					for (int num41 = 0; num41 < 20; num41++)
					{
						int num42 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num42].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num43 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(5f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num43].netUpdate = true;
				}
				if (base.npc.ai[2] == 325f)
				{
					for (int num44 = 0; num44 < 20; num44++)
					{
						int num45 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num45].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num46 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-5f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num46].netUpdate = true;
				}
				if (base.npc.ai[2] == 326f)
				{
					for (int num47 = 0; num47 < 20; num47++)
					{
						int num48 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num48].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num49 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-2f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num49].netUpdate = true;
				}
				if (base.npc.ai[2] == 328f)
				{
					for (int num50 = 0; num50 < 20; num50++)
					{
						int num51 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num51].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num52 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(7f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num52].netUpdate = true;
				}
				if (base.npc.ai[2] == 331f)
				{
					for (int num53 = 0; num53 < 20; num53++)
					{
						int num54 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num54].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num55 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(1f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
				}
				if (base.npc.ai[2] == 332f)
				{
					for (int num56 = 0; num56 < 20; num56++)
					{
						int num57 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num57].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num58 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(-8f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num58].netUpdate = true;
				}
				if (base.npc.ai[2] == 335f)
				{
					for (int num59 = 0; num59 < 20; num59++)
					{
						int num60 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num60].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num61 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(6f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num61].netUpdate = true;
				}
				if (base.npc.ai[2] == 336f)
				{
					for (int num62 = 0; num62 < 20; num62++)
					{
						int num63 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num63].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num64 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(2f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num64].netUpdate = true;
				}
				if (base.npc.ai[2] == 340f)
				{
					for (int num65 = 0; num65 < 20; num65++)
					{
						int num66 = Dust.NewDust(new Vector2(base.npc.position.X + 48f, base.npc.position.Y + 26f), 6, 6, 235, 0f, 0f, 100, default(Color), 1.2f);
						Main.dust[num66].velocity *= 1.9f;
					}
					Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num67 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 32f), new Vector2(0f, -20f), base.mod.ProjectileType("OmegaMissile"), 60, 3f, 255, 0f, 0f);
					Main.projectile[num67].netUpdate = true;
				}
				if (base.npc.ai[2] >= 400f)
				{
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[1] == 1f)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int num68 = 0; num68 < 100; num68++)
				{
					int num69 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num69].velocity *= 1.9f;
				}
				for (int num70 = 0; num70 < 50; num70++)
				{
					int num71 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num71].velocity *= 1.9f;
				}
				for (int num72 = 0; num72 < 50; num72++)
				{
					int num73 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num73].velocity *= 1.9f;
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] == 40f)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int num74 = 0; num74 < 100; num74++)
				{
					int num75 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num75].velocity *= 1.9f;
				}
				for (int num76 = 0; num76 < 50; num76++)
				{
					int num77 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num77].velocity *= 1.9f;
				}
				for (int num78 = 0; num78 < 50; num78++)
				{
					int num79 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num79].velocity *= 1.9f;
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] == 70f)
			{
				Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
				Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
				for (int num80 = 0; num80 < 100; num80++)
				{
					int num81 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num81].velocity *= 1.9f;
				}
				for (int num82 = 0; num82 < 50; num82++)
				{
					int num83 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num83].velocity *= 1.9f;
				}
				for (int num84 = 0; num84 < 50; num84++)
				{
					int num85 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num85].velocity *= 1.9f;
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] == 1f)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "ERROR: POWER OVERLOAD...", true, false);
			}
			if (base.npc.ai[1] == 130f)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "I REQUIRE ASSISTANCE, GIRUS...", true, false);
			}
			if (base.npc.ai[1] == 250f)
			{
				base.npc.ai[0] = 1f;
				base.npc.dontTakeDamage = false;
				base.npc.netUpdate = true;
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
						CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "TARGET OBLITERATED... RETURNING TO GIRUS...", true, false);
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

		private bool charging;

		private bool start;

		private bool beginAttacks;

		private int deadTimer;
	}
}
