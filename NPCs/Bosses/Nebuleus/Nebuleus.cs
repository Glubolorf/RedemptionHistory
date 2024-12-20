using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	[AutoloadBossHead]
	public class Nebuleus : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus, Angel of the Cosmos");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 650000;
			base.npc.defense = 170;
			base.npc.damage = 250;
			base.npc.width = 50;
			base.npc.height = 76;
			base.npc.aiStyle = -1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath59;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossStarGod1");
			base.npc.noTileCollide = false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.55f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void NPCLoot()
		{
			if (!Main.expertMode)
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebuleusTrophy"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebuleusMask"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GildedBonnet"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FreedomStarN"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebulaStarFlail"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ConstellationsBook"), 1, false, 0, false, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("StarfruitSeedbag"), 1, false, 0, false, false);
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				int num = 58;
				int num2 = 8;
				for (int i = 0; i < num2; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num3].noLight = false;
					Main.dust[num3].noGravity = true;
				}
				int num4 = 59;
				int num5 = 10;
				for (int j = 0; j < num5; j++)
				{
					int num6 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num4, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)j / (float)num5 * 6.28f);
					Main.dust[num6].noLight = false;
					Main.dust[num6].noGravity = true;
				}
				int num7 = 60;
				int num8 = 12;
				for (int k = 0; k < num8; k++)
				{
					int num9 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num7, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(18f, 0f), (float)k / (float)num8 * 6.28f);
					Main.dust[num9].noLight = false;
					Main.dust[num9].noGravity = true;
				}
				int num10 = 62;
				int num11 = 14;
				for (int l = 0; l < num11; l++)
				{
					int num12 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num10, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num12].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(20f, 0f), (float)l / (float)num11 * 6.28f);
					Main.dust[num12].noLight = false;
					Main.dust[num12].noGravity = true;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 58, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			if (!Main.expertMode)
			{
				if (!RedeWorld.downedNebuleus)
				{
					RedeWorld.redemptionPoints -= 4;
					CombatText.NewText(this.player.getRect(), Color.Red, "-4", true, false);
					for (int i = 0; i < 255; i++)
					{
						Player player = Main.player[i];
						if (player.active)
						{
							for (int j = 0; j < player.inventory.Length; j++)
							{
								if (player.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
								{
									Main.NewText("<Chalice of Alignment> ... You... Oh no...", Color.DarkGoldenrod, false);
								}
							}
						}
					}
				}
				RedeWorld.downedNebuleus = true;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.75;
			if (damage > 50000.0)
			{
				damage = 1.0;
			}
			if (Main.expertMode && base.npc.life <= (int)((float)base.npc.lifeMax * 0.07f))
			{
				damage = 1.0;
			}
			return true;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
				writer.Write(this.teleportTimer);
				writer.Write(this.beginFight);
				writer.Write(this.teleport);
				writer.Write(this.razzleDazzle);
				writer.Write(this.phase2);
				writer.Write(this.phase3);
				writer.Write(this.phase4);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
				this.teleportTimer = reader.ReadInt32();
				this.beginFight = reader.ReadBool();
				this.teleport = reader.ReadBool();
				this.razzleDazzle = reader.ReadBool();
				this.phase2 = reader.ReadBool();
				this.phase3 = reader.ReadBool();
				this.phase4 = reader.ReadBool();
			}
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (base.npc.ai[0] == 0f)
			{
				NPC npc = base.npc;
				npc.velocity.Y = npc.velocity.Y + 0.005f;
				if (base.npc.velocity.Y > 0.3f)
				{
					base.npc.ai[0] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[0] == 1f)
			{
				NPC npc2 = base.npc;
				npc2.velocity.Y = npc2.velocity.Y - 0.005f;
				if (base.npc.velocity.Y < -0.3f)
				{
					base.npc.ai[0] = 0f;
					base.npc.netUpdate = true;
				}
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc3 = base.npc;
				npc3.frame.Y = npc3.frame.Y + 82;
				if (base.npc.frame.Y > 328)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
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
			if (base.npc.ai[2] != 1f)
			{
				this.customAI[0] += 1f;
			}
			if (this.customAI[0] == 1f)
			{
				this.razzleDazzle = true;
			}
			if (!this.beginFight)
			{
				base.npc.dontTakeDamage = true;
				if (RedeWorld.deathByNeb)
				{
					if (RedeWorld.downedNebuleus)
					{
						if (this.customAI[0] == 60f)
						{
							string text = "You wish to fight me again... ?";
							Color rarityPink = Colors.RarityPink;
							byte r = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text, r, g, rarityPink.B, false);
						}
						if (this.customAI[0] == 180f)
						{
							this.beginFight = true;
							this.customAI[0] = 1400f;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						if (this.customAI[0] == 60f)
						{
							string text2 = "You... are still alive?";
							Color rarityPink = Colors.RarityPink;
							byte r2 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g2 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text2, r2, g2, rarityPink.B, false);
						}
						if (this.customAI[0] == 180f)
						{
							this.beginFight = true;
							this.customAI[0] = 1400f;
							base.npc.netUpdate = true;
						}
					}
				}
				else
				{
					if (RedeWorld.redemptionPoints <= -4)
					{
						if (this.customAI[0] == 60f)
						{
							string text3 = player.name + ", I've been observing you from afar, I've seen the calamity you've caused, the power you've gained...";
							Color rarityPink = Colors.RarityPink;
							byte r3 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g3 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text3, r3, g3, rarityPink.B, false);
						}
						if (this.customAI[0] == 300f)
						{
							string text4 = "I fear I may be too late, but your existance has brought a disequilibrium upon this world...";
							Color rarityPink = Colors.RarityPink;
							byte r4 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g4 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text4, r4, g4, rarityPink.B, false);
						}
						if (this.customAI[0] == 500f)
						{
							if (Redemption.AALoaded)
							{
								string text5 = "Grips of Chaos, Wall of Flesh, Plantera, Moonlord, Equinox Worms... If I don't stop this now, then who will!?";
								Color rarityPink = Colors.RarityPink;
								byte r5 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g5 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text5, r5, g5, rarityPink.B, false);
							}
							else if (Redemption.calamityLoaded)
							{
								string text6 = "Slime God, Wall of Flesh, Plantera, Moonlord, Providence... If I don't stop this now, then who will!?";
								Color rarityPink = Colors.RarityPink;
								byte r6 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g6 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text6, r6, g6, rarityPink.B, false);
							}
							else if (Redemption.thoriumLoaded)
							{
								string text7 = "Viscount, Wall of Flesh, Coznix, Plantera, Moonlord... If I don't stop this now, then who will!?";
								Color rarityPink = Colors.RarityPink;
								byte r7 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g7 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text7, r7, g7, rarityPink.B, false);
							}
							else if (Redemption.spiritLoaded)
							{
								string text8 = "Wall of Flesh, Atlas, Plantera, Moonlord... If I don't stop this now, then who will!?";
								Color rarityPink = Colors.RarityPink;
								byte r8 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g8 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text8, r8, g8, rarityPink.B, false);
							}
							else if (RedeWorld.downedTheKeeper && !Redemption.spiritLoaded && !Redemption.thoriumLoaded && !Redemption.calamityLoaded && !Redemption.AALoaded)
							{
								string text9 = "The Keeper, Wall of Flesh, Plantera, Moonlord... If I don't stop this now, then who will!?";
								Color rarityPink = Colors.RarityPink;
								byte r9 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g9 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text9, r9, g9, rarityPink.B, false);
							}
							else
							{
								string text10 = "Wall of Flesh, Plantera, Moonlord... If I don't stop this now, then who will!?";
								Color rarityPink = Colors.RarityPink;
								byte r10 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g10 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text10, r10, g10, rarityPink.B, false);
							}
						}
						if (this.customAI[0] == 700f)
						{
							string text11 = "Enough chatter, I'm sure you're getting tired of this by now.";
							Color rarityPink = Colors.RarityPink;
							byte r11 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g11 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text11, r11, g11, rarityPink.B, false);
						}
						if (this.customAI[0] == 1000f)
						{
							string text12 = "Just know, " + player.name + ", I am doing this for your sake, if you lose to me... Maybe the Demigod won't notice you.";
							Color rarityPink = Colors.RarityPink;
							byte r12 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g12 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text12, r12, g12, rarityPink.B, false);
						}
					}
					else if (RedeWorld.redemptionPoints > -4 && RedeWorld.redemptionPoints <= 0)
					{
						if (this.customAI[0] == 60f)
						{
							string text13 = player.name + ", I've been observing you from afar, you've been gaining a lot of power...";
							Color rarityPink = Colors.RarityPink;
							byte r13 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g13 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text13, r13, g13, rarityPink.B, false);
						}
						if (this.customAI[0] == 300f)
						{
							string text14 = "I fear I may be too late, but your existance has brought a disequilibrium upon this world...";
							Color rarityPink = Colors.RarityPink;
							byte r14 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g14 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text14, r14, g14, rarityPink.B, false);
						}
						if (this.customAI[0] == 500f)
						{
							string text15 = "Many strong being have been slain by you, if they couldn't stop you, then who will?";
							Color rarityPink = Colors.RarityPink;
							byte r15 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g15 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text15, r15, g15, rarityPink.B, false);
						}
						if (this.customAI[0] == 700f)
						{
							string text16 = "Enough chatter, I'm sure you're getting tired of this by now.";
							Color rarityPink = Colors.RarityPink;
							byte r16 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g16 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text16, r16, g16, rarityPink.B, false);
						}
						if (this.customAI[0] == 1000f)
						{
							string text17 = "Just know, " + player.name + ", I am doing this for your sake, if you lose to me... Maybe the Demigod won't notice you.";
							Color rarityPink = Colors.RarityPink;
							byte r17 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g17 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text17, r17, g17, rarityPink.B, false);
						}
					}
					if (RedeWorld.redemptionPoints > 0)
					{
						if (this.customAI[0] == 60f)
						{
							string text18 = player.name + ", I've been observing you from afar, you've been gaining a lot of power...";
							Color rarityPink = Colors.RarityPink;
							byte r18 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g18 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text18, r18, g18, rarityPink.B, false);
						}
						if (this.customAI[0] == 300f)
						{
							string text19 = "I fear I may be too late, but your existance has brought a disequilibrium upon this world...";
							Color rarityPink = Colors.RarityPink;
							byte r19 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g19 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text19, r19, g19, rarityPink.B, false);
						}
						if (this.customAI[0] == 500f)
						{
							string text20 = "Yes, one might consider it a good thing all these evil being have been slain, but I see it differently.";
							Color rarityPink = Colors.RarityPink;
							byte r20 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g20 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text20, r20, g20, rarityPink.B, false);
						}
						if (this.customAI[0] == 700f)
						{
							string text21 = "Enough chatter, I'm sure you're getting tired of this by now.";
							Color rarityPink = Colors.RarityPink;
							byte r21 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g21 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text21, r21, g21, rarityPink.B, false);
						}
						if (this.customAI[0] == 1000f)
						{
							string text22 = "Just know, " + player.name + ", I am only doing this because I fear you might become too strong to handle.";
							Color rarityPink = Colors.RarityPink;
							byte r22 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g22 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text22, r22, g22, rarityPink.B, false);
						}
					}
					if (this.customAI[0] == 1350f)
					{
						string text23 = "Go, my pet! I will back you up.";
						Color rarityPink = Colors.RarityPink;
						byte r23 = rarityPink.R;
						rarityPink = Colors.RarityPink;
						byte g23 = rarityPink.G;
						rarityPink = Colors.RarityPink;
						Main.NewText(text23, r23, g23, rarityPink.B, false);
						this.beginFight = true;
						NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernHead"), 0, 0f, 0f, 0f, 0f, 255);
						base.npc.netUpdate = true;
					}
				}
			}
			if (this.beginFight)
			{
				if (NPC.AnyNPCs(base.mod.NPCType("StarWyvernHead")))
				{
					base.npc.dontTakeDamage = true;
					if (this.customAI[0] == 1500f || this.customAI[0] == 1700f || this.customAI[0] == 1900f || this.customAI[0] == 1940f)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num = 8;
						for (int i = 0; i < num; i++)
						{
							int num2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar"), 50, 3f, 255, 0f, 0f);
							Main.projectile[num2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)i / (float)num * 6.28f);
							Main.projectile[num2].netUpdate = true;
						}
					}
					if (this.customAI[0] == 1600f || this.customAI[0] == 1800f || this.customAI[0] == 1920f || this.customAI[0] == 1960f)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num3 = 8;
						for (int j = 0; j < num3; j++)
						{
							int num4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStarO"), 50, 3f, 255, 0f, 0f);
							Main.projectile[num4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)j / (float)num3 * 6.28f);
							Main.projectile[num4].netUpdate = true;
						}
					}
					if (this.customAI[0] >= 2000f)
					{
						this.customAI[0] = 1400f;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (base.npc.life > (int)((float)base.npc.lifeMax * 0.07f))
					{
						base.npc.dontTakeDamage = false;
						base.npc.netUpdate = true;
					}
					if (this.customAI[0] < 3000f)
					{
						this.customAI[0] = 3000f;
					}
					if (!RedeWorld.deathByNeb && this.customAI[0] == 3100f)
					{
						string text24 = "My star serpent is slain... And soon, I fear, I will be next.";
						Color rarityPink = Colors.RarityPink;
						byte r24 = rarityPink.R;
						rarityPink = Colors.RarityPink;
						byte g24 = rarityPink.G;
						rarityPink = Colors.RarityPink;
						Main.NewText(text24, r24, g24, rarityPink.B, false);
					}
					if (this.customAI[0] == 3300f)
					{
						RedeWorld.deathByNeb = true;
						base.npc.ai[2] = 1f;
						base.npc.netUpdate = true;
					}
				}
				if (base.npc.ai[2] == 1f)
				{
					if (base.npc.life > (int)((float)base.npc.lifeMax * 0.75f))
					{
						switch (Main.rand.Next(4))
						{
						case 0:
							base.npc.ai[1] = 1f;
							break;
						case 1:
							base.npc.ai[1] = 2f;
							break;
						case 2:
							base.npc.ai[1] = 3f;
							break;
						case 3:
							base.npc.ai[1] = 4f;
							break;
						}
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					else if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.75f) && base.npc.life > (int)((float)base.npc.lifeMax * 0.5f))
					{
						if (!this.phase2)
						{
							base.npc.ai[3] += 1f;
							if (base.npc.ai[3] == 1f)
							{
								Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
								if (RedeWorld.downedNebuleus)
								{
									string text25 = "Why do you want to fight again?";
									Color rarityPink = Colors.RarityPink;
									byte r25 = rarityPink.R;
									rarityPink = Colors.RarityPink;
									byte g25 = rarityPink.G;
									rarityPink = Colors.RarityPink;
									Main.NewText(text25, r25, g25, rarityPink.B, false);
								}
								else
								{
									string text26 = "ARGH! No! Don't come close!";
									Color rarityPink = Colors.RarityPink;
									byte r26 = rarityPink.R;
									rarityPink = Colors.RarityPink;
									byte g26 = rarityPink.G;
									rarityPink = Colors.RarityPink;
									Main.NewText(text26, r26, g26, rarityPink.B, false);
								}
							}
							if (base.npc.ai[3] == 10f || base.npc.ai[3] == 70f || base.npc.ai[3] == 130f || base.npc.ai[3] == 190f)
							{
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3453, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3454, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3455, 1, false, 0, false, false);
							}
							if (base.npc.ai[3] > 200f)
							{
								this.phase2 = true;
								base.npc.ai[3] = 0f;
								base.npc.netUpdate = true;
							}
						}
						if (this.phase2)
						{
							switch (Main.rand.Next(8))
							{
							case 0:
								base.npc.ai[1] = 1f;
								break;
							case 1:
								base.npc.ai[1] = 2f;
								break;
							case 2:
								base.npc.ai[1] = 3f;
								break;
							case 3:
								base.npc.ai[1] = 4f;
								break;
							case 4:
								base.npc.ai[1] = 5f;
								break;
							case 5:
								base.npc.ai[1] = 6f;
								break;
							case 6:
								base.npc.ai[1] = 7f;
								break;
							case 7:
								base.npc.ai[1] = 8f;
								break;
							}
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
					}
					else if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.5f) && base.npc.life > (int)((float)base.npc.lifeMax * 0.25f))
					{
						if (!this.phase3)
						{
							base.npc.ai[3] += 1f;
							if (base.npc.ai[3] == 1f)
							{
								Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
								if (RedeWorld.downedNebuleus)
								{
									string text27 = "Do you just like the challenge?";
									Color rarityPink = Colors.RarityPink;
									byte r27 = rarityPink.R;
									rarityPink = Colors.RarityPink;
									byte g27 = rarityPink.G;
									rarityPink = Colors.RarityPink;
									Main.NewText(text27, r27, g27, rarityPink.B, false);
								}
								else
								{
									string text28 = "Please just give this up!";
									Color rarityPink = Colors.RarityPink;
									byte r28 = rarityPink.R;
									rarityPink = Colors.RarityPink;
									byte g28 = rarityPink.G;
									rarityPink = Colors.RarityPink;
									Main.NewText(text28, r28, g28, rarityPink.B, false);
								}
							}
							if (base.npc.ai[3] == 10f || base.npc.ai[3] == 70f || base.npc.ai[3] == 130f || base.npc.ai[3] == 190f)
							{
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3453, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3454, 1, false, 0, false, false);
								Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3455, 1, false, 0, false, false);
							}
							if (base.npc.ai[3] > 200f)
							{
								this.phase3 = true;
								base.npc.ai[3] = 0f;
								base.npc.netUpdate = true;
							}
						}
						if (this.phase3)
						{
							switch (Main.rand.Next(10))
							{
							case 0:
								base.npc.ai[1] = 1f;
								break;
							case 1:
								base.npc.ai[1] = 2f;
								break;
							case 2:
								base.npc.ai[1] = 3f;
								break;
							case 3:
								base.npc.ai[1] = 4f;
								break;
							case 4:
								base.npc.ai[1] = 5f;
								break;
							case 5:
								base.npc.ai[1] = 6f;
								break;
							case 6:
								base.npc.ai[1] = 7f;
								break;
							case 7:
								base.npc.ai[1] = 8f;
								break;
							case 8:
								base.npc.ai[1] = 9f;
								break;
							case 9:
								base.npc.ai[1] = 10f;
								break;
							}
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
						}
					}
					else if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.25f))
					{
						if (Main.expertMode)
						{
							if (base.npc.life > (int)((float)base.npc.lifeMax * 0.07f))
							{
								if (!this.phase4)
								{
									base.npc.ai[3] += 1f;
									if (base.npc.ai[3] == 1f)
									{
										Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
										if (RedeWorld.downedNebuleus)
										{
											string text29 = "Or do you just want more of my drops?";
											Color rarityPink = Colors.RarityPink;
											byte r29 = rarityPink.R;
											rarityPink = Colors.RarityPink;
											byte g29 = rarityPink.G;
											rarityPink = Colors.RarityPink;
											Main.NewText(text29, r29, g29, rarityPink.B, false);
										}
										else
										{
											string text30 = "Don't you realise what you're doing!?";
											Color rarityPink = Colors.RarityPink;
											byte r30 = rarityPink.R;
											rarityPink = Colors.RarityPink;
											byte g30 = rarityPink.G;
											rarityPink = Colors.RarityPink;
											Main.NewText(text30, r30, g30, rarityPink.B, false);
										}
									}
									if (base.npc.ai[3] == 10f || base.npc.ai[3] == 70f || base.npc.ai[3] == 130f || base.npc.ai[3] == 190f)
									{
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3453, 1, false, 0, false, false);
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3454, 1, false, 0, false, false);
										Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3455, 1, false, 0, false, false);
									}
									if (base.npc.ai[3] > 200f)
									{
										this.phase4 = true;
										base.npc.ai[3] = 0f;
										base.npc.netUpdate = true;
									}
								}
								if (this.phase4)
								{
									switch (Main.rand.Next(11))
									{
									case 0:
										base.npc.ai[1] = 11f;
										break;
									case 1:
										base.npc.ai[1] = 2f;
										break;
									case 2:
										base.npc.ai[1] = 12f;
										break;
									case 3:
										base.npc.ai[1] = 13f;
										break;
									case 4:
										base.npc.ai[1] = 5f;
										break;
									case 5:
										base.npc.ai[1] = 6f;
										break;
									case 6:
										base.npc.ai[1] = 7f;
										break;
									case 7:
										base.npc.ai[1] = 8f;
										break;
									case 8:
										base.npc.ai[1] = 9f;
										break;
									case 9:
										base.npc.ai[1] = 10f;
										break;
									case 10:
										base.npc.ai[1] = 14f;
										break;
									}
									base.npc.ai[2] = 0f;
									base.npc.netUpdate = true;
								}
							}
						}
						else
						{
							if (!this.phase4)
							{
								base.npc.ai[3] += 1f;
								if (base.npc.ai[3] == 1f)
								{
									Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
									if (RedeWorld.downedNebuleus)
									{
										string text31 = "Or do you just want more of my drops?";
										Color rarityPink = Colors.RarityPink;
										byte r31 = rarityPink.R;
										rarityPink = Colors.RarityPink;
										byte g31 = rarityPink.G;
										rarityPink = Colors.RarityPink;
										Main.NewText(text31, r31, g31, rarityPink.B, false);
									}
									else
									{
										string text32 = "Don't you realise what you're doing!?";
										Color rarityPink = Colors.RarityPink;
										byte r32 = rarityPink.R;
										rarityPink = Colors.RarityPink;
										byte g32 = rarityPink.G;
										rarityPink = Colors.RarityPink;
										Main.NewText(text32, r32, g32, rarityPink.B, false);
									}
								}
								if (base.npc.ai[3] == 10f || base.npc.ai[3] == 70f || base.npc.ai[3] == 130f || base.npc.ai[3] == 190f)
								{
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3453, 1, false, 0, false, false);
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3454, 1, false, 0, false, false);
									Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3455, 1, false, 0, false, false);
								}
								if (base.npc.ai[3] > 200f)
								{
									this.phase4 = true;
									base.npc.ai[3] = 0f;
									base.npc.netUpdate = true;
								}
							}
							if (this.phase4)
							{
								switch (Main.rand.Next(11))
								{
								case 0:
									base.npc.ai[1] = 11f;
									break;
								case 1:
									base.npc.ai[1] = 2f;
									break;
								case 2:
									base.npc.ai[1] = 12f;
									break;
								case 3:
									base.npc.ai[1] = 13f;
									break;
								case 4:
									base.npc.ai[1] = 5f;
									break;
								case 5:
									base.npc.ai[1] = 6f;
									break;
								case 6:
									base.npc.ai[1] = 7f;
									break;
								case 7:
									base.npc.ai[1] = 8f;
									break;
								case 8:
									base.npc.ai[1] = 9f;
									break;
								case 9:
									base.npc.ai[1] = 10f;
									break;
								case 10:
									base.npc.ai[1] = 14f;
									break;
								}
								base.npc.ai[2] = 0f;
								base.npc.netUpdate = true;
							}
						}
					}
					if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.07f) && Main.expertMode)
					{
						base.npc.dontTakeDamage = true;
						base.npc.ai[3] += 1f;
						if (base.npc.ai[3] == 1f)
						{
							Main.PlaySound(SoundID.NPCDeath59, (int)base.npc.position.X, (int)base.npc.position.Y);
							string text33 = "Stop!";
							Color rarityPink = Colors.RarityPink;
							byte r33 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g33 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text33, r33, g33, rarityPink.B, false);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[3] == 10f)
						{
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3453, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3454, 1, false, 0, false, false);
							Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3455, 1, false, 0, false, false);
						}
						if (RedeWorld.downedNebuleus)
						{
							if (base.npc.ai[3] == 120f)
							{
								string text34 = "Jeez, you really want to go through this again...";
								Color rarityPink = Colors.RarityPink;
								byte r34 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g34 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text34, r34, g34, rarityPink.B, false);
								base.npc.ai[3] = 400f;
							}
							if (base.npc.ai[3] >= 400f)
							{
								this.musicChange1 = false;
								this.musicChange2 = true;
								base.npc.netUpdate = true;
							}
							else
							{
								this.musicChange1 = true;
							}
							if (base.npc.ai[3] == 600f)
							{
								string text35 = "All this fighting frustrates me.";
								Color rarityPink = Colors.RarityPink;
								byte r35 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g35 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text35, r35, g35, rarityPink.B, false);
							}
							if (base.npc.ai[3] == 800f)
							{
								string text36 = "Before that dumb robot attacked me, I haven't fought in years.";
								Color rarityPink = Colors.RarityPink;
								byte r36 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g36 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text36, r36, g36, rarityPink.B, false);
							}
							if (base.npc.ai[3] == 940f)
							{
								string text37 = "In a way, I guess this is practice...";
								Color rarityPink = Colors.RarityPink;
								byte r37 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g37 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text37, r37, g37, rarityPink.B, false);
							}
							if (base.npc.ai[3] == 963f)
							{
								int num5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("Transition"), 0, 1f, Main.myPlayer, 0f, 0f);
								Main.projectile[num5].netUpdate = true;
							}
							if (base.npc.ai[3] > 1090f)
							{
								base.npc.SetDefaults(base.mod.NPCType("BigNebuleus"), -1f);
								base.npc.netUpdate = true;
							}
						}
						else
						{
							if (base.npc.ai[3] == 120f)
							{
								string text38 = "I can see now, I am too late...";
								Color rarityPink = Colors.RarityPink;
								byte r38 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g38 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text38, r38, g38, rarityPink.B, false);
							}
							if (base.npc.ai[3] == 240f)
							{
								string text39 = "I knew you'd win. Even if I kill you, you'll just come back.";
								Color rarityPink = Colors.RarityPink;
								byte r39 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g39 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text39, r39, g39, rarityPink.B, false);
							}
							if (base.npc.ai[3] == 400f)
							{
								string text40 = "...";
								Color rarityPink = Colors.RarityPink;
								byte r40 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g40 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text40, r40, g40, rarityPink.B, false);
							}
							if (base.npc.ai[3] >= 400f)
							{
								this.musicChange1 = false;
								this.musicChange2 = true;
								base.npc.netUpdate = true;
							}
							else
							{
								this.musicChange1 = true;
							}
							if (base.npc.ai[3] == 600f)
							{
								string text41 = "Since I know there's no hope...";
								Color rarityPink = Colors.RarityPink;
								byte r41 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g41 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text41, r41, g41, rarityPink.B, false);
							}
							if (base.npc.ai[3] == 800f)
							{
								string text42 = "I guess it wouldn't hurt...";
								Color rarityPink = Colors.RarityPink;
								byte r42 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g42 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text42, r42, g42, rarityPink.B, false);
							}
							if (base.npc.ai[3] == 940f)
							{
								string text43 = "To give it all I've got!";
								Color rarityPink = Colors.RarityPink;
								byte r43 = rarityPink.R;
								rarityPink = Colors.RarityPink;
								byte g43 = rarityPink.G;
								rarityPink = Colors.RarityPink;
								Main.NewText(text43, r43, g43, rarityPink.B, false);
							}
							if (base.npc.ai[3] == 963f)
							{
								int num6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("Transition"), 0, 1f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].netUpdate = true;
							}
							if (base.npc.ai[3] > 1090f)
							{
								base.npc.SetDefaults(base.mod.NPCType("BigNebuleus"), -1f);
								base.npc.netUpdate = true;
							}
						}
					}
				}
			}
			if (base.npc.ai[1] == 1f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout1"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num7].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 120f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num8 = 8;
					for (int k = 0; k < num8; k++)
					{
						int num9 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)k / (float)num8 * 6.28f);
						Main.projectile[num9].netUpdate = true;
					}
				}
				if (base.npc.ai[3] == 90f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num10 = 8;
					for (int l = 0; l < num10; l++)
					{
						int num11 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStarO"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num11].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)l / (float)num10 * 6.28f);
						Main.projectile[num11].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 220f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 2f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num12 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout2"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num12].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] <= 90f && Main.rand.Next(2) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int num13 = Main.rand.Next(-50, 50);
					int num14 = Main.rand.Next(-200, 200) - 1000;
					int num15 = Projectile.NewProjectile(player.Center.X + (float)num13, player.Center.Y + (float)num14, 0f, 13f, base.mod.ProjectileType("StarFallPro"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num15].netUpdate = true;
				}
				if (base.npc.ai[3] >= 160f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 3f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num16 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout3"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num16].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 90f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num17 = 14f;
					Vector2 vector;
					vector..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num18 = 50;
					int num19 = base.mod.ProjectileType("PNebula1");
					int num20 = base.mod.ProjectileType("PNebula2");
					int num21 = base.mod.ProjectileType("PNebula3");
					float num22 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num23 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num22) * (double)num17 * -1.0), (float)(Math.Sin((double)num22) * (double)num17 * -1.0), num19, num18, 0f, 0, 0f, 0f);
					int num24 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num22) * (double)num17 * -1.0), (float)(Math.Sin((double)num22) * (double)num17 * -1.0), num20, num18, 0f, 0, 0f, 0f);
					int num25 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num22) * (double)num17 * -1.0), (float)(Math.Sin((double)num22) * (double)num17 * -1.0), num21, num18, 0f, 0, 0f, 0f);
					Main.projectile[num23].netUpdate = true;
					Main.projectile[num24].netUpdate = true;
					Main.projectile[num25].netUpdate = true;
				}
				if (base.npc.ai[3] >= 140f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 4f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num26 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout4"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num26].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f)
				{
					Main.PlaySound(SoundID.Item121, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num27 = 7f;
					Vector2 vector2;
					vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num28 = 40;
					int num29 = base.mod.ProjectileType("StarVortex1");
					int num30 = base.mod.ProjectileType("StarVortex2");
					float num31 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num32 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num31) * (double)num27 * -1.0), (float)(Math.Sin((double)num31) * (double)num27 * -1.0), num29, num28, 0f, 0, 0f, 0f);
					int num33 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num31) * (double)num27 * -1.0), (float)(Math.Sin((double)num31) * (double)num27 * -1.0), num30, num28, 0f, 0, 0f, 0f);
					Main.projectile[num32].netUpdate = true;
					Main.projectile[num33].netUpdate = true;
				}
				if (base.npc.ai[3] >= 140f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 5f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num34 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout5"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num34].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] < 120f)
				{
					if (Main.rand.Next(20) == 0)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num35 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("CurvingStar"), 50, 1f, 255, 0f, 0f);
						Main.projectile[num35].netUpdate = true;
					}
					if (Main.rand.Next(20) == 0)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num36 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("CurvingStarO"), 50, 1f, 255, 0f, 0f);
						Main.projectile[num36].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 160f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 6f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num37 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout6"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num37].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num38 = 8;
					for (int m = 0; m < num38; m++)
					{
						int num39 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula1"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num39].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)m / (float)num38 * 6.28f);
						int num40 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula2"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num40].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)m / (float)num38 * 6.28f);
						int num41 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula3"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num41].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)m / (float)num38 * 6.28f);
						Main.projectile[num39].netUpdate = true;
						Main.projectile[num40].netUpdate = true;
						Main.projectile[num41].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 80f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 7f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num42 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout7"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num42].netUpdate = true;
				}
				if (base.npc.ai[3] == 20f)
				{
					int num43 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					int num44 = Projectile.NewProjectile(base.npc.Center.X - 100f, base.npc.Center.Y + 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					int num45 = Projectile.NewProjectile(base.npc.Center.X + 100f, base.npc.Center.Y + 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num43].netUpdate = true;
					Main.projectile[num44].netUpdate = true;
					Main.projectile[num45].netUpdate = true;
				}
				if (base.npc.ai[3] >= 100f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 8f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num46 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout8"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num46].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] <= 160f && Main.rand.Next(4) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int num47 = Main.rand.Next(-200, 200) * 6;
					int num48 = Main.rand.Next(-200, 200) - 1000;
					int num49 = Projectile.NewProjectile(player.Center.X + (float)num47, player.Center.Y + (float)num48, 2f, 6f, base.mod.ProjectileType("StarFallPro"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num49].netUpdate = true;
				}
				if (base.npc.ai[3] >= 160f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 9f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num50 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout9"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num50].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 90f || base.npc.ai[3] == 120f || base.npc.ai[3] == 150f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num51 = 26f;
					Vector2 vector3;
					vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num52 = 10;
					int num53 = base.mod.ProjectileType("CosmosChain1");
					float num54 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num54) * (double)num51 * -1.0), (float)(Math.Sin((double)num54) * (double)num51 * -1.0), num53, num52, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
				}
				if (base.npc.ai[3] >= 150f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 10f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num56 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout10"), 0, 1f, Main.myPlayer, 0f, 0f);
					int num57 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("NebRing"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num56].netUpdate = true;
					Main.projectile[num57].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f)
				{
					int num58 = NPC.NewNPC((int)base.npc.Center.X - 100, (int)base.npc.Center.Y + 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num58].netUpdate = true;
				}
				if (base.npc.ai[3] == 70f)
				{
					int num59 = NPC.NewNPC((int)base.npc.Center.X - 100, (int)base.npc.Center.Y - 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num59].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f)
				{
					int num60 = NPC.NewNPC((int)base.npc.Center.X + 100, (int)base.npc.Center.Y - 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num60].netUpdate = true;
				}
				if (base.npc.ai[3] == 90f)
				{
					int num61 = NPC.NewNPC((int)base.npc.Center.X + 100, (int)base.npc.Center.Y + 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num61].netUpdate = true;
				}
				if (base.npc.ai[3] == 100f)
				{
					int num62 = NPC.NewNPC((int)base.npc.Center.X + 200, (int)base.npc.Center.Y, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num62].netUpdate = true;
				}
				if (base.npc.ai[3] == 110f)
				{
					int num63 = NPC.NewNPC((int)base.npc.Center.X - 200, (int)base.npc.Center.Y, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num63].netUpdate = true;
				}
				if (base.npc.ai[3] == 120f)
				{
					int num64 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 200, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num64].netUpdate = true;
				}
				if (base.npc.ai[3] == 130f)
				{
					int num65 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y - 200, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num65].netUpdate = true;
				}
				if (base.npc.ai[3] >= 240f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 11f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num66 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout1"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num66].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 120f || base.npc.ai[3] == 180f || base.npc.ai[3] == 250f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num67 = 8;
					for (int n = 0; n < num67; n++)
					{
						int num68 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar2"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num68].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)n / (float)num67 * 6.28f);
						Main.projectile[num68].netUpdate = true;
					}
				}
				if (base.npc.ai[3] == 90f || base.npc.ai[3] == 210f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num69 = 8;
					for (int num70 = 0; num70 < num69; num70++)
					{
						int num71 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar2O"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num71].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)num70 / (float)num69 * 6.28f);
						Main.projectile[num71].netUpdate = true;
					}
				}
				if (base.npc.ai[3] >= 340f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 12f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num72 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout3"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num72].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 70f || base.npc.ai[3] == 80f || base.npc.ai[3] == 90f || base.npc.ai[3] == 100f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num73 = 15f;
					Vector2 vector4;
					vector4..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num74 = 50;
					int num75 = base.mod.ProjectileType("PNebula1");
					int num76 = base.mod.ProjectileType("PNebula2");
					int num77 = base.mod.ProjectileType("PNebula3");
					float num78 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					int num79 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num78) * (double)num73 * -1.0), (float)(Math.Sin((double)num78) * (double)num73 * -1.0), num75, num74, 0f, 0, 0f, 0f);
					int num80 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num78) * (double)num73 * -1.0), (float)(Math.Sin((double)num78) * (double)num73 * -1.0), num76, num74, 0f, 0, 0f, 0f);
					int num81 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num78) * (double)num73 * -1.0), (float)(Math.Sin((double)num78) * (double)num73 * -1.0), num77, num74, 0f, 0, 0f, 0f);
					Main.projectile[num79].netUpdate = true;
					Main.projectile[num80].netUpdate = true;
					Main.projectile[num81].netUpdate = true;
				}
				if (base.npc.ai[3] >= 140f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 13f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num82 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout4"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num82].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f || base.npc.ai[3] == 140f)
				{
					Main.PlaySound(SoundID.Item121, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num83 = 7f;
					Vector2 vector5;
					vector5..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num84 = 40;
					int num85 = base.mod.ProjectileType("StarVortex1");
					int num86 = base.mod.ProjectileType("StarVortex2");
					float num87 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
					int num88 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num87) * (double)num83 * -1.0), (float)(Math.Sin((double)num87) * (double)num83 * -1.0), num85, num84, 0f, 0, 0f, 0f);
					int num89 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num87) * (double)num83 * -1.0), (float)(Math.Sin((double)num87) * (double)num83 * -1.0), num86, num84, 0f, 0, 0f, 0f);
					Main.projectile[num88].netUpdate = true;
					Main.projectile[num89].netUpdate = true;
				}
				if (base.npc.ai[3] >= 200f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			else if (base.npc.ai[1] == 14f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num90 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout11"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num90].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] <= 120f && Main.rand.Next(20) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int num91 = Main.rand.Next(-200, 200) * 6;
					int num92 = Main.rand.Next(-200, 200) - 1000;
					int num93 = Projectile.NewProjectile(player.Center.X + (float)num91, player.Center.Y + (float)num92, 2f, 6f, base.mod.ProjectileType("CrystalStarPro1"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num93].netUpdate = true;
				}
				if (base.npc.ai[3] >= 160f)
				{
					base.npc.ai[3] = 0f;
					base.npc.ai[1] = 0f;
					this.teleport = true;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
			}
			float num94 = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num94 >= 950f && !this.teleport)
			{
				this.teleport = true;
				base.npc.netUpdate = true;
			}
			if (this.teleport)
			{
				base.npc.netUpdate = true;
				this.teleportTimer++;
				if (this.teleportTimer == 2)
				{
					this.razzleDazzle = true;
				}
				if (this.teleportTimer == 4 && Main.netMode != 1)
				{
					int num95 = Main.rand.Next(2);
					if (num95 == 0)
					{
						Vector2 vector6;
						vector6..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + vector6;
						base.npc.netUpdate = true;
					}
					if (num95 == 1)
					{
						Vector2 vector7;
						vector7..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + vector7;
						base.npc.netUpdate = true;
					}
				}
				if (this.teleportTimer >= 6)
				{
					this.razzleDazzle = true;
					this.teleport = false;
					this.teleportTimer = 0;
					base.npc.netUpdate = true;
				}
			}
			if (this.razzleDazzle)
			{
				int num96 = 58;
				int num97 = 8;
				for (int num98 = 0; num98 < num97; num98++)
				{
					int num99 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num96, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num99].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)num98 / (float)num97 * 6.28f);
					Main.dust[num99].noLight = false;
					Main.dust[num99].noGravity = true;
				}
				int num100 = 59;
				int num101 = 10;
				for (int num102 = 0; num102 < num101; num102++)
				{
					int num103 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num100, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num103].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)num102 / (float)num101 * 6.28f);
					Main.dust[num103].noLight = false;
					Main.dust[num103].noGravity = true;
				}
				int num104 = 60;
				int num105 = 12;
				for (int num106 = 0; num106 < num105; num106++)
				{
					int num107 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num104, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num107].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)num106 / (float)num105 * 6.28f);
					Main.dust[num107].noLight = false;
					Main.dust[num107].noGravity = true;
				}
				int num108 = 62;
				int num109 = 14;
				for (int num110 = 0; num110 < num109; num110++)
				{
					int num111 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num108, 0f, 0f, 100, Color.White, 3f);
					Main.dust[num111].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)num110 / (float)num109 * 6.28f);
					Main.dust[num111].noLight = false;
					Main.dust[num111].noGravity = true;
				}
				base.npc.netUpdate = true;
				this.razzleDazzle = false;
			}
			if (this.musicChange1)
			{
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
			}
			if (this.musicChange2)
			{
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossStarGod2");
			}
			if (Main.expertMode && base.npc.life <= (int)((float)base.npc.lifeMax * 0.07f))
			{
				base.npc.immortal = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			base.mod.GetTexture("NPCs/Bosses/Nebuleus/Nebuleus_Wings");
			Vector2 vector = base.npc.Center - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY);
			int shaderIdFromItemId = GameShaders.Armor.GetShaderIdFromItemId(2870);
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/Nebuleus/Nebuleus_Wings");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			spriteBatch.Draw(texture2D, vector, new Rectangle?(base.npc.frame), base.npc.GetAlpha(drawColor), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			BaseDrawing.DrawTexture(spriteBatch, texture, shaderIdFromItemId, base.npc, new Color?(base.npc.GetAlpha(Color.White)), true, Utils.Size(base.npc.frame) / 2f);
			return false;
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
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
				}
			}
		}

		private Player player;

		public float[] customAI = new float[1];

		private int teleportTimer;

		private bool beginFight;

		private bool teleport;

		private bool razzleDazzle;

		private bool musicChange1;

		private bool musicChange2;

		private bool phase2;

		private bool phase3;

		private bool phase4;
	}
}
