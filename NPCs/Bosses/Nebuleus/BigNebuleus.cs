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
	public class BigNebuleus : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus, Angel of the Cosmos");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 350000;
			base.npc.defense = 170;
			base.npc.damage = 250;
			base.npc.width = 82;
			base.npc.height = 116;
			base.npc.aiStyle = -1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath59;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossStarGod2");
			base.npc.noTileCollide = false;
			this.bossBag = base.mod.ItemType("NebBag");
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("NebuleusTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.55f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				int num = 58;
				int num2 = 8;
				for (int i = 0; i < num2; i++)
				{
					int num3 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num, 0f, 0f, 100, Color.White, 5f);
					Main.dust[num3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)num2 * 6.28f);
					Main.dust[num3].noLight = false;
					Main.dust[num3].noGravity = true;
				}
				int num4 = 59;
				int num5 = 10;
				for (int j = 0; j < num5; j++)
				{
					int num6 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num4, 0f, 0f, 100, Color.White, 5f);
					Main.dust[num6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)j / (float)num5 * 6.28f);
					Main.dust[num6].noLight = false;
					Main.dust[num6].noGravity = true;
				}
				int num7 = 60;
				int num8 = 12;
				for (int k = 0; k < num8; k++)
				{
					int num9 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num7, 0f, 0f, 100, Color.White, 5f);
					Main.dust[num9].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(18f, 0f), (float)k / (float)num8 * 6.28f);
					Main.dust[num9].noLight = false;
					Main.dust[num9].noGravity = true;
				}
				int num10 = 62;
				int num11 = 14;
				for (int l = 0; l < num11; l++)
				{
					int num12 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num10, 0f, 0f, 100, Color.White, 5f);
					Main.dust[num12].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(20f, 0f), (float)l / (float)num11 * 6.28f);
					Main.dust[num12].noLight = false;
					Main.dust[num12].noGravity = true;
				}
				int num13 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("NebFalling"), 0, 3f, 255, 0f, 0f);
				Main.npc[num13].netUpdate = true;
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 58, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.7;
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
				npc3.frame.Y = npc3.frame.Y + 186;
				if (base.npc.frame.Y > 558)
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
				if (RedeWorld.downedNebuleus)
				{
					if (this.customAI[0] == 80f)
					{
						string text = "Listen, I'm still a little concerned by how much power you've gained...";
						Color rarityPink = Colors.RarityPink;
						byte r = rarityPink.R;
						rarityPink = Colors.RarityPink;
						byte g = rarityPink.G;
						rarityPink = Colors.RarityPink;
						Main.NewText(text, r, g, rarityPink.B, false);
					}
					if (this.customAI[0] == 340f)
					{
						string text2 = "So I'll still give it my all.";
						Color rarityPink = Colors.RarityPink;
						byte r2 = rarityPink.R;
						rarityPink = Colors.RarityPink;
						byte g2 = rarityPink.G;
						rarityPink = Colors.RarityPink;
						Main.NewText(text2, r2, g2, rarityPink.B, false);
					}
					if (this.customAI[0] == 500f)
					{
						string text3 = "Now, time to do this again...";
						Color rarityPink = Colors.RarityPink;
						byte r3 = rarityPink.R;
						rarityPink = Colors.RarityPink;
						byte g3 = rarityPink.G;
						rarityPink = Colors.RarityPink;
						Main.NewText(text3, r3, g3, rarityPink.B, false);
					}
					if (this.customAI[0] == 700f)
					{
						this.beginFight = true;
						base.npc.netUpdate = true;
					}
				}
				else
				{
					if (this.customAI[0] == 80f)
					{
						string text4 = "Even if you defeat me, there are many others stronger than myself!";
						Color rarityPink = Colors.RarityPink;
						byte r4 = rarityPink.R;
						rarityPink = Colors.RarityPink;
						byte g4 = rarityPink.G;
						rarityPink = Colors.RarityPink;
						Main.NewText(text4, r4, g4, rarityPink.B, false);
					}
					if (Redemption.AALoaded && Redemption.calamityLoaded)
					{
						if (this.customAI[0] == 280f)
						{
							this.CalamityBosses();
						}
						if (this.customAI[0] == 340f)
						{
							this.AABosses();
						}
						if (this.customAI[0] == 400f)
						{
							string text5 = "... and the Demigod of Light...";
							Color rarityPink = Colors.RarityPink;
							byte r5 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g5 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text5, r5, g5, rarityPink.B, false);
						}
					}
					else if (Redemption.calamityLoaded)
					{
						if (this.customAI[0] == 280f)
						{
							this.CalamityBosses();
						}
						if (this.customAI[0] == 340f)
						{
							string text6 = "... and the Demigod of Light...";
							Color rarityPink = Colors.RarityPink;
							byte r6 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g6 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text6, r6, g6, rarityPink.B, false);
						}
					}
					else if (Redemption.AALoaded)
					{
						if (this.customAI[0] == 280f)
						{
							this.AABosses();
						}
						if (this.customAI[0] == 340f)
						{
							string text7 = "... and the Demigod of Light...";
							Color rarityPink = Colors.RarityPink;
							byte r7 = rarityPink.R;
							rarityPink = Colors.RarityPink;
							byte g7 = rarityPink.G;
							rarityPink = Colors.RarityPink;
							Main.NewText(text7, r7, g7, rarityPink.B, false);
						}
					}
					else if (this.customAI[0] == 300f)
					{
						string text8 = "You wouldn't last a second against the Demigod of Light...";
						Color rarityPink = Colors.RarityPink;
						byte r8 = rarityPink.R;
						rarityPink = Colors.RarityPink;
						byte g8 = rarityPink.G;
						rarityPink = Colors.RarityPink;
						Main.NewText(text8, r8, g8, rarityPink.B, false);
					}
					if (this.customAI[0] == 500f)
					{
						string text9 = "But enough talk, I'm your opponent!";
						Color rarityPink = Colors.RarityPink;
						byte r9 = rarityPink.R;
						rarityPink = Colors.RarityPink;
						byte g9 = rarityPink.G;
						rarityPink = Colors.RarityPink;
						Main.NewText(text9, r9, g9, rarityPink.B, false);
					}
					if (this.customAI[0] == 700f)
					{
						this.beginFight = true;
						base.npc.netUpdate = true;
					}
				}
			}
			if (this.beginFight)
			{
				if (this.customAI[0] >= 3180f && this.customAI[0] <= 6390f && Main.rand.Next(60) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int num = Main.rand.Next(-200, 200) * 6;
					int num2 = Main.rand.Next(-200, 200) - 1000;
					int num3 = Projectile.NewProjectile(player.Center.X + (float)num, player.Center.Y + (float)num2, 2f, 6f, base.mod.ProjectileType("StarFallPro2"), 45, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num3].netUpdate = true;
				}
				if (this.customAI[0] >= 6390f && Main.rand.Next(50) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int num4 = Main.rand.Next(-200, 200) - 1000;
					int num5 = Main.rand.Next(-200, 200) * 4;
					int num6 = Projectile.NewProjectile(player.Center.X + (float)num4, player.Center.Y + (float)num5, 6f, 0f, base.mod.ProjectileType("StarFallPro2"), 45, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num6].netUpdate = true;
				}
				base.npc.dontTakeDamage = false;
				if (this.customAI[0] == 750f)
				{
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 1f)
				{
					switch (Main.rand.Next(9))
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
					}
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[1] == 4f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout5"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num7].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] < 120f)
				{
					if (Main.rand.Next(20) == 0)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num8 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("CurvingStar2"), 50, 1f, 255, 0f, 0f);
						Main.projectile[num8].netUpdate = true;
					}
					if (Main.rand.Next(20) == 0)
					{
						Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
						int num9 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), base.mod.ProjectileType("CurvingStar2O"), 50, 1f, 255, 0f, 0f);
						Main.projectile[num9].netUpdate = true;
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
			else if (base.npc.ai[1] == 5f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num10 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout6"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num10].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 90f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num11 = 8;
					for (int i = 0; i < num11; i++)
					{
						int num12 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula1"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num12].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)i / (float)num11 * 6.28f);
						int num13 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula2"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num13].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)i / (float)num11 * 6.28f);
						int num14 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PNebula3"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num14].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)i / (float)num11 * 6.28f);
						Main.projectile[num12].netUpdate = true;
						Main.projectile[num13].netUpdate = true;
						Main.projectile[num14].netUpdate = true;
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
			else if (base.npc.ai[1] == 6f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num15 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout7"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num15].netUpdate = true;
				}
				if (base.npc.ai[3] == 20f)
				{
					int num16 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					int num17 = Projectile.NewProjectile(base.npc.Center.X - 100f, base.npc.Center.Y + 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					int num18 = Projectile.NewProjectile(base.npc.Center.X + 100f, base.npc.Center.Y + 100f, 0f, 0f, base.mod.ProjectileType("NebPortal1"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num16].netUpdate = true;
					Main.projectile[num17].netUpdate = true;
					Main.projectile[num18].netUpdate = true;
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
			else if (base.npc.ai[1] == 7f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num19 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout9"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num19].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 80f || base.npc.ai[3] == 100f || base.npc.ai[3] == 120f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num20 = 26f;
					Vector2 vector;
					vector..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num21 = 10;
					int num22 = base.mod.ProjectileType("CosmosChain1");
					float num23 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num24 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num23) * (double)num20 * -1.0), (float)(Math.Sin((double)num23) * (double)num20 * -1.0), num22, num21, 0f, 0, 0f, 0f);
					Main.projectile[num24].netUpdate = true;
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
			else if (base.npc.ai[1] == 8f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num25 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout10"), 0, 1f, Main.myPlayer, 0f, 0f);
					int num26 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("NebRing"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num25].netUpdate = true;
					Main.projectile[num26].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f)
				{
					int num27 = NPC.NewNPC((int)base.npc.Center.X - 100, (int)base.npc.Center.Y + 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num27].netUpdate = true;
				}
				if (base.npc.ai[3] == 70f)
				{
					int num28 = NPC.NewNPC((int)base.npc.Center.X - 100, (int)base.npc.Center.Y - 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num28].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f)
				{
					int num29 = NPC.NewNPC((int)base.npc.Center.X + 100, (int)base.npc.Center.Y - 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num29].netUpdate = true;
				}
				if (base.npc.ai[3] == 90f)
				{
					int num30 = NPC.NewNPC((int)base.npc.Center.X + 100, (int)base.npc.Center.Y + 100, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num30].netUpdate = true;
				}
				if (base.npc.ai[3] == 100f)
				{
					int num31 = NPC.NewNPC((int)base.npc.Center.X + 200, (int)base.npc.Center.Y, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num31].netUpdate = true;
				}
				if (base.npc.ai[3] == 110f)
				{
					int num32 = NPC.NewNPC((int)base.npc.Center.X - 200, (int)base.npc.Center.Y, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num32].netUpdate = true;
				}
				if (base.npc.ai[3] == 120f)
				{
					int num33 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 200, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num33].netUpdate = true;
				}
				if (base.npc.ai[3] == 130f)
				{
					int num34 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y - 200, base.mod.NPCType("NebEye1"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num34].netUpdate = true;
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
			else if (base.npc.ai[1] == 1f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num35 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout1"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num35].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 120f || base.npc.ai[3] == 180f || base.npc.ai[3] == 250f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num36 = 8;
					for (int j = 0; j < num36; j++)
					{
						int num37 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar2"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num37].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)j / (float)num36 * 6.28f);
						Main.projectile[num37].netUpdate = true;
					}
				}
				if (base.npc.ai[3] == 90f || base.npc.ai[3] == 210f)
				{
					Main.PlaySound(SoundID.Item117, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num38 = 8;
					for (int k = 0; k < num38; k++)
					{
						int num39 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("CurvingStar2O"), 50, 3f, 255, 0f, 0f);
						Main.projectile[num39].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), (float)k / (float)num38 * 6.28f);
						Main.projectile[num39].netUpdate = true;
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
			else if (base.npc.ai[1] == 2f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num40 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout3"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num40].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f || base.npc.ai[3] == 70f || base.npc.ai[3] == 80f || base.npc.ai[3] == 90f || base.npc.ai[3] == 100f)
				{
					Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num41 = 15f;
					Vector2 vector2;
					vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num42 = 50;
					int num43 = base.mod.ProjectileType("PNebula1");
					int num44 = base.mod.ProjectileType("PNebula2");
					int num45 = base.mod.ProjectileType("PNebula3");
					float num46 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num47 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num46) * (double)num41 * -1.0), (float)(Math.Sin((double)num46) * (double)num41 * -1.0), num43, num42, 0f, 0, 0f, 0f);
					int num48 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num46) * (double)num41 * -1.0), (float)(Math.Sin((double)num46) * (double)num41 * -1.0), num44, num42, 0f, 0, 0f, 0f);
					int num49 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num46) * (double)num41 * -1.0), (float)(Math.Sin((double)num46) * (double)num41 * -1.0), num45, num42, 0f, 0, 0f, 0f);
					Main.projectile[num47].netUpdate = true;
					Main.projectile[num48].netUpdate = true;
					Main.projectile[num49].netUpdate = true;
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
			else if (base.npc.ai[1] == 3f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num50 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout4"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num50].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f || base.npc.ai[3] == 140f)
				{
					Main.PlaySound(SoundID.Item121, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num51 = 7f;
					Vector2 vector3;
					vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num52 = 40;
					int num53 = base.mod.ProjectileType("StarVortex1");
					int num54 = base.mod.ProjectileType("StarVortex2");
					float num55 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					int num56 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num55) * (double)num51 * -1.0), (float)(Math.Sin((double)num55) * (double)num51 * -1.0), num53, num52, 0f, 0, 0f, 0f);
					int num57 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num55) * (double)num51 * -1.0), (float)(Math.Sin((double)num55) * (double)num51 * -1.0), num54, num52, 0f, 0, 0f, 0f);
					Main.projectile[num56].netUpdate = true;
					Main.projectile[num57].netUpdate = true;
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
			else if (base.npc.ai[1] == 9f)
			{
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 5f)
				{
					int num58 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y - 100f, 0f, 0f, base.mod.ProjectileType("Shout11"), 0, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num58].netUpdate = true;
				}
				if (base.npc.ai[3] >= 60f && base.npc.ai[3] <= 120f && Main.rand.Next(20) == 0)
				{
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 8, 1f, 0f);
					int num59 = Main.rand.Next(-200, 200) * 6;
					int num60 = Main.rand.Next(-200, 200) - 1000;
					int num61 = Projectile.NewProjectile(player.Center.X + (float)num59, player.Center.Y + (float)num60, 2f, 6f, base.mod.ProjectileType("CrystalStarPro1"), 50, 1f, Main.myPlayer, 0f, 0f);
					Main.projectile[num61].netUpdate = true;
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
			float num62 = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num62 >= 950f && !this.teleport)
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
					int num63 = Main.rand.Next(2);
					if (num63 == 0)
					{
						Vector2 vector4;
						vector4..ctor((float)Main.rand.Next(-400, -250), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + vector4;
						base.npc.netUpdate = true;
					}
					if (num63 == 1)
					{
						Vector2 vector5;
						vector5..ctor((float)Main.rand.Next(250, 400), (float)Main.rand.Next(-200, 50));
						base.npc.Center = Main.player[base.npc.target].Center + vector5;
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
				int num64 = 58;
				int num65 = 8;
				for (int l = 0; l < num65; l++)
				{
					int num66 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num64, 0f, 0f, 100, Color.White, 4f);
					Main.dust[num66].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)num65 * 6.28f);
					Main.dust[num66].noLight = false;
					Main.dust[num66].noGravity = true;
				}
				int num67 = 59;
				int num68 = 10;
				for (int m = 0; m < num68; m++)
				{
					int num69 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num67, 0f, 0f, 100, Color.White, 4f);
					Main.dust[num69].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)m / (float)num68 * 6.28f);
					Main.dust[num69].noLight = false;
					Main.dust[num69].noGravity = true;
				}
				int num70 = 60;
				int num71 = 12;
				for (int n = 0; n < num71; n++)
				{
					int num72 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num70, 0f, 0f, 100, Color.White, 4f);
					Main.dust[num72].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(14f, 0f), (float)n / (float)num71 * 6.28f);
					Main.dust[num72].noLight = false;
					Main.dust[num72].noGravity = true;
				}
				int num73 = 62;
				int num74 = 14;
				for (int num75 = 0; num75 < num74; num75++)
				{
					int num76 = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, num73, 0f, 0f, 100, Color.White, 4f);
					Main.dust[num76].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(16f, 0f), (float)num75 / (float)num74 * 6.28f);
					Main.dust[num76].noLight = false;
					Main.dust[num76].noGravity = true;
				}
				this.razzleDazzle = false;
				base.npc.netUpdate = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Vector2 vector = base.npc.Center - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY);
			int shaderIdFromItemId = GameShaders.Armor.GetShaderIdFromItemId(2870);
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/Nebuleus/BigNebuleus_Wings");
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

		public void AABosses()
		{
			switch (Main.rand.Next(8))
			{
			case 0:
			{
				string text = "...the Hero of Legend...";
				Color rarityPink = Colors.RarityPink;
				byte r = rarityPink.R;
				Color rarityPink2 = Colors.RarityPink;
				byte g = rarityPink2.G;
				Color rarityPink3 = Colors.RarityPink;
				Main.NewText(text, r, g, rarityPink3.B, false);
				return;
			}
			case 1:
			{
				string text2 = "...the giant of steel and darkness...";
				Color rarityPink4 = Colors.RarityPink;
				byte r2 = rarityPink4.R;
				Color rarityPink5 = Colors.RarityPink;
				byte g2 = rarityPink5.G;
				Color rarityPink6 = Colors.RarityPink;
				Main.NewText(text2, r2, g2, rarityPink6.B, false);
				return;
			}
			case 2:
			{
				string text3 = "...the forgotten soul of the destroyer titan...";
				Color rarityPink7 = Colors.RarityPink;
				byte r3 = rarityPink7.R;
				Color rarityPink8 = Colors.RarityPink;
				byte g3 = rarityPink8.G;
				Color rarityPink9 = Colors.RarityPink;
				Main.NewText(text3, r3, g3, rarityPink9.B, false);
				return;
			}
			case 3:
			{
				string text4 = "...the overmind of fungus...";
				Color rarityPink10 = Colors.RarityPink;
				byte r4 = rarityPink10.R;
				Color rarityPink11 = Colors.RarityPink;
				byte g4 = rarityPink11.G;
				Color rarityPink12 = Colors.RarityPink;
				Main.NewText(text4, r4, g4, rarityPink12.B, false);
				return;
			}
			case 4:
			{
				string text5 = "...the evil warden of that horrid dungeon...";
				Color rarityPink13 = Colors.RarityPink;
				byte r5 = rarityPink13.R;
				Color rarityPink14 = Colors.RarityPink;
				byte g5 = rarityPink14.G;
				Color rarityPink15 = Colors.RarityPink;
				Main.NewText(text5, r5, g5, rarityPink15.B, false);
				return;
			}
			case 5:
			{
				string text6 = "...the Emperor of Discord...";
				Color rarityPink16 = Colors.RarityPink;
				byte r6 = rarityPink16.R;
				Color rarityPink17 = Colors.RarityPink;
				byte g6 = rarityPink17.G;
				Color rarityPink18 = Colors.RarityPink;
				Main.NewText(text6, r6, g6, rarityPink18.B, false);
				return;
			}
			case 6:
			{
				string text7 = "...the shadow of death...";
				Color rarityPink19 = Colors.RarityPink;
				byte r7 = rarityPink19.R;
				Color rarityPink20 = Colors.RarityPink;
				byte g7 = rarityPink20.G;
				Color rarityPink21 = Colors.RarityPink;
				Main.NewText(text7, r7, g7, rarityPink21.B, false);
				return;
			}
			case 7:
			{
				string text8 = "...he who created all...";
				Color rarityPink22 = Colors.RarityPink;
				byte r8 = rarityPink22.R;
				Color rarityPink23 = Colors.RarityPink;
				byte g8 = rarityPink23.G;
				Color rarityPink24 = Colors.RarityPink;
				Main.NewText(text8, r8, g8, rarityPink24.B, false);
				return;
			}
			default:
				return;
			}
		}

		public void CalamityBosses()
		{
			switch (Main.rand.Next(6))
			{
			case 0:
			{
				string text = "...the Tyrant of the Jungle...";
				Color rarityPink = Colors.RarityPink;
				byte r = rarityPink.R;
				Color rarityPink2 = Colors.RarityPink;
				byte g = rarityPink2.G;
				Color rarityPink3 = Colors.RarityPink;
				Main.NewText(text, r, g, rarityPink3.B, false);
				return;
			}
			case 1:
			{
				string text2 = "...the witch of unyielding fury...";
				Color rarityPink4 = Colors.RarityPink;
				byte r2 = rarityPink4.R;
				Color rarityPink5 = Colors.RarityPink;
				byte g2 = rarityPink5.G;
				Color rarityPink6 = Colors.RarityPink;
				Main.NewText(text2, r2, g2, rarityPink6.B, false);
				return;
			}
			case 2:
			{
				string text3 = "...the sphere of terror...";
				Color rarityPink7 = Colors.RarityPink;
				byte r3 = rarityPink7.R;
				Color rarityPink8 = Colors.RarityPink;
				byte g3 = rarityPink8.G;
				Color rarityPink9 = Colors.RarityPink;
				Main.NewText(text3, r3, g3, rarityPink9.B, false);
				return;
			}
			case 3:
			{
				string text4 = "...the tyrant's dragon...";
				Color rarityPink10 = Colors.RarityPink;
				byte r4 = rarityPink10.R;
				Color rarityPink11 = Colors.RarityPink;
				byte g4 = rarityPink11.G;
				Color rarityPink12 = Colors.RarityPink;
				Main.NewText(text4, r4, g4, rarityPink12.B, false);
				return;
			}
			case 4:
				break;
			case 5:
			{
				string text5 = "...the cosmic blacksmith...";
				Color rarityPink13 = Colors.RarityPink;
				byte r5 = rarityPink13.R;
				Color rarityPink14 = Colors.RarityPink;
				byte g5 = rarityPink14.G;
				Color rarityPink15 = Colors.RarityPink;
				Main.NewText(text5, r5, g5, rarityPink15.B, false);
				return;
			}
			case 6:
			{
				string text6 = "...the god of unimaginable power...";
				Color rarityPink16 = Colors.RarityPink;
				byte r6 = rarityPink16.R;
				Color rarityPink17 = Colors.RarityPink;
				byte g6 = rarityPink17.G;
				Color rarityPink18 = Colors.RarityPink;
				Main.NewText(text6, r6, g6, rarityPink18.B, false);
				break;
			}
			default:
				return;
			}
		}

		private Player player;

		private bool beginFight;

		private bool teleport;

		private int teleportTimer;

		private bool razzleDazzle;

		public float[] customAI = new float[1];
	}
}
